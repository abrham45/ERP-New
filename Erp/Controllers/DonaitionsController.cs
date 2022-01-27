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
    public class DonaitionsController : Controller
    {
        private readonly EmployeeContext _context;

        public DonaitionsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Donaitions
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Donaition.Include(d => d.Asset);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Donaitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donaition = await _context.Donaition
                .Include(d => d.Asset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donaition == null)
            {
                return NotFound();
            }

            return View(donaition);
        }

        // GET: Donaitions/Create
        public IActionResult Create()
        {
          
            return View();
        }

        // POST: Donaitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonaitedTo,AssetId,Reason,TransferDate")] Donaition donaition)
        {
            var factNum = Convert.ToString(HttpContext.Request.Form["AssetId"]);
            var asset = _context.Asset.FirstOrDefault(e => e.factory_number == factNum);
            donaition.TransferDate = DateTime.Today;
            if (asset != null)
            {
                _context.Add(donaition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                TempData["Error"] = " Invalid Asset or Asset Factory Number";
                return RedirectToAction(nameof(Create));
            }
         
        }

        // GET: Donaitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donaition = await _context.Donaition.FindAsync(id);
            if (donaition == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Asset_Name", donaition.AssetId);
            return View(donaition);
        }

        // POST: Donaitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DonaitedTo,AssetId,Reason,TransferDate")] Donaition donaition)
        {
            if (id != donaition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donaition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonaitionExists(donaition.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Asset_Name", donaition.AssetId);
            return View(donaition);
        }

        // GET: Donaitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donaition = await _context.Donaition
                .Include(d => d.Asset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donaition == null)
            {
                return NotFound();
            }

            return View(donaition);
        }

        // POST: Donaitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donaition = await _context.Donaition.FindAsync(id);
            _context.Donaition.Remove(donaition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonaitionExists(int id)
        {
            return _context.Donaition.Any(e => e.Id == id);
        }
    }
}
