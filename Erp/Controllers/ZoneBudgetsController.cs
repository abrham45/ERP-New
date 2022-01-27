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
    public class ZoneBudgetsController : Controller
    {
        private readonly EmployeeContext _context;

        public ZoneBudgetsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: ZoneBudgets
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.ZoneBudget.Include(z => z.Zone).Include(z => z.ZonePlan);
            return View(await employeeContext.ToListAsync());
        }

        // GET: ZoneBudgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneBudget = await _context.ZoneBudget
                .Include(z => z.Zone)
                .Include(z => z.ZonePlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zoneBudget == null)
            {
                return NotFound();
            }

            return View(zoneBudget);
        }

        // GET: ZoneBudgets/Create
        public IActionResult Create()
        {
            ViewData["ZoneId"] = new SelectList(_context.Zone, "Id", "FirstName");
            ViewData["ZonePlanId"] = new SelectList(_context.ZonePlan, "Id", "Id");
            return View();
        }

        // POST: ZoneBudgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,OverheadCosts,TimeCosts,ExternalCost,ZoneTotalCost,Status,ZonePlanId,ZoneId")] ZoneBudget zoneBudget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zoneBudget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZoneId"] = new SelectList(_context.Zone, "Id", "FirstName", zoneBudget.ZoneId);
            ViewData["ZonePlanId"] = new SelectList(_context.ZonePlan, "Id", "Id", zoneBudget.ZonePlanId);
            return View(zoneBudget);
        }

        // GET: ZoneBudgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneBudget = await _context.ZoneBudget.FindAsync(id);
            if (zoneBudget == null)
            {
                return NotFound();
            }
            ViewData["ZoneId"] = new SelectList(_context.Zone, "Id", "FirstName", zoneBudget.ZoneId);
            ViewData["ZonePlanId"] = new SelectList(_context.ZonePlan, "Id", "Id", zoneBudget.ZonePlanId);
            return View(zoneBudget);
        }

        // POST: ZoneBudgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,OverheadCosts,TimeCosts,ExternalCost,ZoneTotalCost,Status,ZonePlanId,ZoneId")] ZoneBudget zoneBudget)
        {
            if (id != zoneBudget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zoneBudget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZoneBudgetExists(zoneBudget.Id))
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
            ViewData["ZoneId"] = new SelectList(_context.Zone, "Id", "FirstName", zoneBudget.ZoneId);
            ViewData["ZonePlanId"] = new SelectList(_context.ZonePlan, "Id", "Id", zoneBudget.ZonePlanId);
            return View(zoneBudget);
        }

        // GET: ZoneBudgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneBudget = await _context.ZoneBudget
                .Include(z => z.Zone)
                .Include(z => z.ZonePlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zoneBudget == null)
            {
                return NotFound();
            }

            return View(zoneBudget);
        }

        // POST: ZoneBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zoneBudget = await _context.ZoneBudget.FindAsync(id);
            _context.ZoneBudget.Remove(zoneBudget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneBudgetExists(int id)
        {
            return _context.ZoneBudget.Any(e => e.Id == id);
        }
    }
}
