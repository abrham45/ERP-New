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

namespace Erp.Controllers
{
    public class TerminationsController : Controller
    {/*
        [Authorize(Roles = "HR-Admin")]*/
        private readonly EmployeeContext _context;

        public TerminationsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Terminations
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Termination.Include(t => t.Employee);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Terminations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termination = await _context.Termination
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (termination == null)
            {
                return NotFound();
            }

            return View(termination);
        }

        // GET: Terminations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Terminations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reason,EmployeeId")] Termination termination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "EmployeeCode", termination.EmployeeId);
            return View(termination);
        }

        // GET: Terminations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termination = await _context.Termination.FindAsync(id);
            if (termination == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "EmployeeCode", termination.EmployeeId);
            return View(termination);
        }

        // POST: Terminations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reason,EmployeeId")] Termination termination)
        {
            if (id != termination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminationExists(termination.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "EmployeeCode", termination.EmployeeId);
            return View(termination);
        }

        // GET: Terminations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termination = await _context.Termination
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (termination == null)
            {
                return NotFound();
            }

            return View(termination);
        }

        // POST: Terminations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var termination = await _context.Termination.FindAsync(id);
            _context.Termination.Remove(termination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminationExists(int id)
        {
            return _context.Termination.Any(e => e.Id == id);
        }
    }
}
