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
    public class AdvancedSchemesController : Controller
    {
        private readonly EmployeeContext _context;

        public AdvancedSchemesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: AdvancedSchemes
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.AdvancedScheme.Include(a => a.Employee);
            return View(await employeeContext.ToListAsync());
        }

        // GET: AdvancedSchemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advancedScheme = await _context.AdvancedScheme
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advancedScheme == null)
            {
                return NotFound();
            }

            return View(advancedScheme);
        }

        // GET: AdvancedSchemes/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City");
            return View();
        }

        // POST: AdvancedSchemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,PercentageAmount,DeductionEachMonth,DurationRecover,EmployeeId,SalarId")] AdvancedScheme advancedScheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advancedScheme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City", advancedScheme.EmployeeId);
            return View(advancedScheme);
        }

        // GET: AdvancedSchemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advancedScheme = await _context.AdvancedScheme.FindAsync(id);
            if (advancedScheme == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City", advancedScheme.EmployeeId);
            return View(advancedScheme);
        }

        // POST: AdvancedSchemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,PercentageAmount,DeductionEachMonth,DurationRecover,EmployeeId,SalarId")] AdvancedScheme advancedScheme)
        {
            if (id != advancedScheme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advancedScheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvancedSchemeExists(advancedScheme.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City", advancedScheme.EmployeeId);
            return View(advancedScheme);
        }

        // GET: AdvancedSchemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advancedScheme = await _context.AdvancedScheme
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advancedScheme == null)
            {
                return NotFound();
            }

            return View(advancedScheme);
        }

        // POST: AdvancedSchemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advancedScheme = await _context.AdvancedScheme.FindAsync(id);
            _context.AdvancedScheme.Remove(advancedScheme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvancedSchemeExists(int id)
        {
            return _context.AdvancedScheme.Any(e => e.Id == id);
        }
    }
}
