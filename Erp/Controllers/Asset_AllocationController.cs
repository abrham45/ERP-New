using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using Erp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Erp.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Erp.Controllers
{
    public class Asset_AllocationController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
       
        public Asset_AllocationController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Asset_Allocation
        [Authorize(Roles = "Inventory")]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            
           return View(await PaginatedList<Asset_Allocation>.CreateAsync(_context.Asset_Allocation, pageNumber, 1));
        }
        public async Task<IActionResult> Indexuser( int pageNumber = 1)
        {

            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if(assetal != null)
            {
                var empall = _context.Asset_Allocation.Where(e => e.EmployeeId == assetal.Id).Include(e => e.Employee).Include(e => e.Asset);
                return View(await PaginatedList<Asset_Allocation>.CreateAsync(empall, pageNumber, 1));
            }
            else
            {
                return NotFound();
            }
          
        }
        // GET: Asset_Allocation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_Allocation = await _context.Asset_Allocation
                .Include(q=>q.Asset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset_Allocation == null)
            {
                return NotFound();
            }

            return View(asset_Allocation);
        }

        // GET: Asset_Allocation/Create
        public async Task<IActionResult> Create()
        {

            User users = await _userManager.GetUserAsync(User);
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AssetId = new SelectList(_context.Asset, "Id", "Asset_Name");
                return View();
            }
        }
        [Authorize(Roles = "Inventory")]
        // POST: Asset_Allocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssetId,Description")] Asset_Allocation asset_Allocation)
        {
            var employeecode = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var asset = Convert.ToString(HttpContext.Request.Form["AssetId"]);
            var empc = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employeecode);
            var assets = _context.Asset.FirstOrDefault(e => e.factory_number == asset);

         
     
                    if (empc != null & assets != null)
                    {
           
                asset_Allocation.EmployeeId = empc.Id;
                asset_Allocation.AssetId = assets.Id;

                _context.Add(asset_Allocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] =" Invalid Employee code or Factory Number";
                return RedirectToAction(nameof(Create));
            }

        }
        [Authorize(Roles = "Inventory")]
        // GET: Asset_Allocation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_Allocation = await _context.Asset_Allocation.FindAsync(id);
            if (asset_Allocation == null)
            {
                return NotFound();
            }

            ViewBag.AssetId = new SelectList(_context.Asset, "Id", "Assnet_Name", asset_Allocation.AssetId);
            return View(asset_Allocation);
        }
        [Authorize(Roles = "Inventory")]
        // POST: Asset_Allocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,AssetId,Description")] Asset_Allocation asset_Allocation)
        {
            if (id != asset_Allocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset_Allocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Asset_AllocationExists(asset_Allocation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.AssetId = new SelectList(_context.Asset, "Id", "Assnet_Name", asset_Allocation.AssetId);
            return View(asset_Allocation);
        }

        // GET: Asset_Allocation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_Allocation = await _context.Asset_Allocation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset_Allocation == null)
            {
                return NotFound();
            }

            return View(asset_Allocation);
        }

        // POST: Asset_Allocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset_Allocation = await _context.Asset_Allocation.FindAsync(id);
            _context.Asset_Allocation.Remove(asset_Allocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Asset_AllocationExists(int id)
        {
            return _context.Asset_Allocation.Any(e => e.Id == id);
        }
    }
}
