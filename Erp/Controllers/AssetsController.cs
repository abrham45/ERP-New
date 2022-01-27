using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;
using PagedList;
using Erp;

namespace Erp.Controllers
{
    public class AssetsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public AssetsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Assets
        [Authorize(Roles = "StoreManager, Director")]
        public async Task<IActionResult> Index(string sth, int pageNumber=1)
        {
            var asst = from a in _context.Asset.Include(a => a.Asset_type) select a;

            if (!String.IsNullOrEmpty(sth))
            {
                asst = asst.Where(s => s.factory_number.Contains(sth));
            }

            return View(await PaginatedList<Asset>.CreateAsync(asst, pageNumber,22));
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset.Include(a=>a.Asset_type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewBag.Asset_typeId = new SelectList(_context.Asset_type, "Id", "Asset_Type");
            return View(asset);
        }

        // GET: Assets/Create
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
                ViewBag.Asset_typeId = new SelectList(_context.Asset_type, "Id", "Asset_Type");
                ViewBag.PaymentId = new SelectList(_context.Payment, "Id", "Price");
                ViewBag.SupplierId = new SelectList(_context.Supplier, "Id", "CompanyName");

                return View();
            }
            
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "StoreManager")]
        public async Task<IActionResult> Create([Bind("Id,Asset_Name,Asset_typeId,factory_number,serial_number,Price,SupplierId")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Asset_typeId = new SelectList(_context.Asset_type, "Id", "Asset_Type", asset.Asset_typeId);

          /*  ViewBag.PaymentId = new SelectList(_context.Payment, "Id", "Price", asset.PaymentId);*/

            ViewBag.SupplierId = new SelectList(_context.Supplier, "Id", "CompanyName",asset.SupplierId);
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewBag.SupplierId = new SelectList(_context.Supplier, "Id", "CompanyName");
            ViewBag.Asset_typeId = new SelectList(_context.Asset_type, "Id", "Asset_Type");
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Asset_Name,Asset_typeId,factory_number,serial_number,Price,SupplierId")] Asset asset)
        {
            if (id != asset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.Id))
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
            ViewBag.SupplierId = new SelectList(_context.Supplier, "Id", "CompanyName", asset.SupplierId);
            ViewBag.Asset_typeId = new SelectList(_context.Asset_type, "Id", "Asset_Type", asset.Asset_typeId);
            return View(asset);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Asset.FindAsync(id);
            _context.Asset.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.Id == id);
        }
    }
}
