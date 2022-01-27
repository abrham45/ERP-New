using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;

namespace Erp.Controllers
{
    public class AssetLoansController : Controller
    {
        private readonly EmployeeContext _context;

        public AssetLoansController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: AssetLoans
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.AssetLoan.Include(a => a.Asset);
            return View(await employeeContext.ToListAsync());
        }

        // GET: AssetLoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetLoan = await _context.AssetLoan
                .Include(a => a.Asset)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetLoan == null)
            {
                return NotFound();
            }

            return View(assetLoan);
        }

        // GET: AssetLoans/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: AssetLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,FaultyAssetId,AssetId,Reason,TransferDate")] AssetLoan assetLoan)
        {
            var factNum = Convert.ToString(HttpContext.Request.Form["AssetId"]);
            var asset = _context.Asset.FirstOrDefault(e => e.factory_number == factNum);
            assetLoan.TransferDate = DateTime.Today;
            if (asset != null)
            {
                assetLoan.AssetId = asset.Id;
                _context.Add(assetLoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                TempData["Error"] = " Invalid Asset or Asset Factory Number";
                return RedirectToAction(nameof(Create));
            }
       
        }

        // GET: AssetLoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetLoan = await _context.AssetLoan.FindAsync(id);

            if (assetLoan == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Asset_Name", assetLoan.AssetId);
            ViewData["FaultyAssetId"] = new SelectList(_context.FaultyAsset, "Id", "Id", assetLoan);
            return View(assetLoan);
        }

        // POST: AssetLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,FaultyAssetId,AssetId,Reason,TransferDate")] AssetLoan assetLoan)
        {
            if (id != assetLoan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetLoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetLoanExists(assetLoan.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Asset_Name", assetLoan.AssetId);
            ViewData["FaultyAssetId"] = new SelectList(_context.FaultyAsset, "Id", "Id");
            return View(assetLoan);
        }

        // GET: AssetLoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetLoan = await _context.AssetLoan
                .Include(a => a.Asset)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetLoan == null)
            {
                return NotFound();
            }

            return View(assetLoan);
        }

        // POST: AssetLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetLoan = await _context.AssetLoan.FindAsync(id);
            _context.AssetLoan.Remove(assetLoan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetLoanExists(int id)
        {
            return _context.AssetLoan.Any(e => e.Id == id);
        }
    }
}
