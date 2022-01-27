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
    public class AssetDesposalsController : Controller
    {
        private readonly EmployeeContext _context;

        public AssetDesposalsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: AssetDesposals
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.AssetDesposal.Include(a => a.Asset);
            return View(await employeeContext.ToListAsync());
        }

        // GET: AssetDesposals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetDesposal = await _context.AssetDesposal
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetDesposal == null)
            {
                return NotFound();
            }

            return View(assetDesposal);
        }

        // GET: AssetDesposals/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: AssetDesposals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssetId,Reason,TransferDate")] AssetDesposal assetDesposal)
        {
            var factNum = Convert.ToString(HttpContext.Request.Form["AssetId"]);
            var asset = _context.Asset.FirstOrDefault(e => e.factory_number == factNum);
            assetDesposal.TransferDate = DateTime.Today;
            if (asset != null)
            {
                assetDesposal.AssetId = asset.Id;
                _context.Add(assetDesposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                TempData["Error"] = " Invalid Asset or Asset Factory Number";
                return RedirectToAction(nameof(Create));
            }
       
        }


        // GET: AssetDesposals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetDesposal = await _context.AssetDesposal.FindAsync(id);
            if (assetDesposal == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Asset_Name", assetDesposal.AssetId);
            return View(assetDesposal);
        }

        // POST: AssetDesposals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssetId,Reason,TransferDate")] AssetDesposal assetDesposal)
        {
            if (id != assetDesposal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetDesposal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetDesposalExists(assetDesposal.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Asset_Name", assetDesposal.AssetId);
            return View(assetDesposal);
        }

        // GET: AssetDesposals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetDesposal = await _context.AssetDesposal
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetDesposal == null)
            {
                return NotFound();
            }

            return View(assetDesposal);
        }

        // POST: AssetDesposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetDesposal = await _context.AssetDesposal.FindAsync(id);
            _context.AssetDesposal.Remove(assetDesposal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetDesposalExists(int id)
        {
            return _context.AssetDesposal.Any(e => e.Id == id);
        }
    }
}
