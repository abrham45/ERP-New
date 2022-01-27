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
    public class RegionBudgetsController : Controller
    {
        private readonly EmployeeContext _context;

        public RegionBudgetsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: RegionBudgets
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.RegionBudget.Include(r => r.Region).Include(r => r.RegionPlan);
            return View(await employeeContext.ToListAsync());
        }

        // GET: RegionBudgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionBudget = await _context.RegionBudget
                .Include(r => r.Region)
                .Include(r => r.RegionPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regionBudget == null)
            {
                return NotFound();
            }

            return View(regionBudget);
        }

        // GET: RegionBudgets/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName");
            ViewData["RegionPlanId"] = new SelectList(_context.RegionPlan, "Id", "Id");
            return View();
        }

        // POST: RegionBudgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,TotalRegionOverheadCosts,TotalRegionTimeCosts,TotalRegionExternalCost,TotalRegionZoneTotalCost,Status,RegionPlanId,RegionId")] RegionBudget regionBudget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regionBudget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", regionBudget.RegionId);
            ViewData["RegionPlanId"] = new SelectList(_context.RegionPlan, "Id", "Id", regionBudget.RegionPlanId);
            return View(regionBudget);
        }

        // GET: RegionBudgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionBudget = await _context.RegionBudget.FindAsync(id);
            if (regionBudget == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", regionBudget.RegionId);
            ViewData["RegionPlanId"] = new SelectList(_context.RegionPlan, "Id", "Id", regionBudget.RegionPlanId);
            return View(regionBudget);
        }

        // POST: RegionBudgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,TotalRegionOverheadCosts,TotalRegionTimeCosts,TotalRegionExternalCost,TotalRegionZoneTotalCost,Status,RegionPlanId,RegionId")] RegionBudget regionBudget)
        {
            if (id != regionBudget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regionBudget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionBudgetExists(regionBudget.Id))
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
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", regionBudget.RegionId);
            ViewData["RegionPlanId"] = new SelectList(_context.RegionPlan, "Id", "Id", regionBudget.RegionPlanId);
            return View(regionBudget);
        }

        // GET: RegionBudgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionBudget = await _context.RegionBudget
                .Include(r => r.Region)
                .Include(r => r.RegionPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regionBudget == null)
            {
                return NotFound();
            }

            return View(regionBudget);
        }

        // POST: RegionBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regionBudget = await _context.RegionBudget.FindAsync(id);
            _context.RegionBudget.Remove(regionBudget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionBudgetExists(int id)
        {
            return _context.RegionBudget.Any(e => e.Id == id);
        }
    }
}
