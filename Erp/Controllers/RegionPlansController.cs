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
    public class RegionPlansController : Controller
    {
        private readonly EmployeeContext _context;

        public RegionPlansController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: RegionPlans
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.RegionPlan.Include(r => r.Region);
            return View(await employeeContext.ToListAsync());
        }

        // GET: RegionPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionPlan = await _context.RegionPlan
                .Include(r => r.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regionPlan == null)
            {
                return NotFound();
            }

            return View(regionPlan);
        }

        // GET: RegionPlans/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName");
            return View();
        }

        // POST: RegionPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FiscalYear,Period,Description,ProjectNumber,Status,OperationExpense,EffectiveDate,RegionId")] RegionPlan regionPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regionPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", regionPlan.RegionId);
            return View(regionPlan);
        }

        // GET: RegionPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionPlan = await _context.RegionPlan.FindAsync(id);
            if (regionPlan == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", regionPlan.RegionId);
            return View(regionPlan);
        }

        // POST: RegionPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FiscalYear,Period,Description,ProjectNumber,Status,OperationExpense,EffectiveDate,RegionId")] RegionPlan regionPlan)
        {
            if (id != regionPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regionPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionPlanExists(regionPlan.Id))
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
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", regionPlan.RegionId);
            return View(regionPlan);
        }

        // GET: RegionPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionPlan = await _context.RegionPlan
                .Include(r => r.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regionPlan == null)
            {
                return NotFound();
            }

            return View(regionPlan);
        }

        // POST: RegionPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regionPlan = await _context.RegionPlan.FindAsync(id);
            _context.RegionPlan.Remove(regionPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionPlanExists(int id)
        {
            return _context.RegionPlan.Any(e => e.Id == id);
        }
    }
}
