using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;

namespace Erp.Controllers
{
    public class Asset_ExchangeController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public Asset_ExchangeController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Asset_Exchange
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Asset_Exchange.Include(a => a.Asset);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Asset_Exchange/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_Exchange = await _context.Asset_Exchange
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset_Exchange == null)
            {
                return NotFound();
            }

            return View(asset_Exchange);
        }

        // GET: Asset_Exchange/Create
        public  IActionResult Create()
        {
           return View(); 
        }

        // POST: Asset_Exchange/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asset_Exchange asset_exchange)
        {
            
                
                User users = await _userManager.GetUserAsync(User);
                var empfrom = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

                var asset = Convert.ToString(HttpContext.Request.Form["AssetId"]);
                var assets = _context.Asset.FirstOrDefault(e => e.factory_number == asset);
            if (assets != null)
            {
                var ass_emp = _context.Asset_Allocation.FirstOrDefault(e => e.EmployeeId == empfrom.Id & e.AssetId == assets.Id);



                var employeecode = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
                var empto = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employeecode);



                if (ass_emp != null & empfrom != null & empto != null)
                {


                    asset_exchange.from = empfrom.Id;
                    asset_exchange.EmployeeId = empto.Id;
                    asset_exchange.AssetId = ass_emp.AssetId;
                    asset_exchange.status = null;


                    _context.Add(asset_exchange);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create));
                }

                else
                {
                    TempData["Error"] = " Invalid Employee code or Asset Factory Number";
                    return RedirectToAction(nameof(Create));
                }

            }
            else
            {
                TempData["Error"] = " Empty Employee code or Asset Factory Number";
                return RedirectToAction(nameof(Create));
            }
                    

        }

        // GET: Asset_Exchange/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_Exchange = await _context.Asset_Exchange.FindAsync(id);
            if (asset_Exchange == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Id", asset_Exchange.AssetId);
            return View(asset_Exchange);
        }

        // POST: Asset_Exchange/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approved(int id)
        {

            var asset_Exchange = await _context.Asset_Exchange.FindAsync(id);

            if (asset_Exchange == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    asset_Exchange.status = true;
                    var asset_all = _context.Asset_Allocation.FirstOrDefault(e => e.EmployeeId == asset_Exchange.from);
                    asset_all.EmployeeId = asset_Exchange.EmployeeId;

                    _context.Update(asset_all);
                    await _context.SaveChangesAsync();

                    _context.Update(asset_Exchange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Asset_ExchangeExists(asset_Exchange.Id))
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

        }
/// <summary>
/// /
/// </summary>
/// <param name="id"></param>
/// <returns></returns> 
        public async Task<IActionResult> Reject(int id)
        {
            var asset_Exchange = await _context.Asset_Exchange.FindAsync(id);

            if (asset_Exchange == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    asset_Exchange.status = false;
                    _context.Update(asset_Exchange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Asset_ExchangeExists(asset_Exchange.Id))
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
        }
        // GET: Asset_Exchange/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_Exchange = await _context.Asset_Exchange
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset_Exchange == null)
            {
                return NotFound();
            }

            return View(asset_Exchange);
        }

        // POST: Asset_Exchange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset_Exchange = await _context.Asset_Exchange.FindAsync(id);
            _context.Asset_Exchange.Remove(asset_Exchange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Asset_ExchangeExists(int id)
        {
            return _context.Asset_Exchange.Any(e => e.Id == id);
        }
    }
}
