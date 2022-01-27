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
    public class ZonePlansController : Controller
    {
        private readonly EmployeeContext _context;

        public ZonePlansController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: ZonePlans
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.ZonePlan.Include(z => z.PlanType).Include(z => z.Zone);
            return View(await employeeContext.ToListAsync());
        }

        // GET: ZonePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zonePlan = await _context.ZonePlan
                .Include(z => z.PlanType)
                .Include(z => z.Zone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zonePlan == null)
            {
                return NotFound();
            }

            return View(zonePlan);
        }

        // GET: ZonePlans/Create
        public IActionResult Create()
        {
            ViewData["PlanTypeId"] = new SelectList(_context.PlanType, "Id", "Id");
            ViewData["ZoneId"] = new SelectList(_context.Zone, "Id", "FirstName");
            return View();
        }

        // POST: ZonePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlanName,FiscalYear,PlanTypeId,Period,Description,ProjectNumber,Status,OperationExpense,ZoneId")] ZonePlan zonePlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zonePlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanTypeId"] = new SelectList(_context.PlanType, "Id", "Id", zonePlan.PlanTypeId);
            ViewData["ZoneId"] = new SelectList(_context.Zone, "Id", "FirstName", zonePlan.ZoneId);
            return View(zonePlan);
        }

        // GET: ZonePlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zonePlan = await _context.ZonePlan.FindAsync(id);
            if (zonePlan == null)
            {
                return NotFound();
            }
            ViewData["PlanTypeId"] = new SelectList(_context.PlanType, "Id", "Id", zonePlan.PlanTypeId);
            ViewData["ZoneId"] = new SelectList(_context.Zone, "Id", "FirstName", zonePlan.ZoneId);
            return View(zonePlan);
        }

        // POST: ZonePlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanName,FiscalYear,PlanTypeId,Period,Description,ProjectNumber,Status,OperationExpense,ZoneId")] ZonePlan zonePlan)
        {
            if (id != zonePlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zonePlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZonePlanExists(zonePlan.Id))
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
            ViewData["PlanTypeId"] = new SelectList(_context.PlanType, "Id", "Id", zonePlan.PlanTypeId);
            ViewData["ZoneId"] = new SelectList(_context.Zone, "Id", "FirstName", zonePlan.ZoneId);
            return View(zonePlan);
        }

        // GET: ZonePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zonePlan = await _context.ZonePlan
                .Include(z => z.PlanType)
                .Include(z => z.Zone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zonePlan == null)
            {
                return NotFound();
            }

            return View(zonePlan);
        }

        // POST: ZonePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zonePlan = await _context.ZonePlan.FindAsync(id);
            _context.ZonePlan.Remove(zonePlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZonePlanExists(int id)
        {
            return _context.ZonePlan.Any(e => e.Id == id);
        }
    }
}
