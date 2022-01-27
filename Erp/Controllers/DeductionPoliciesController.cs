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
    [Authorize(Roles = "FinanceTeam")]
    public class DeductionPoliciesController : Controller
    {
        private readonly EmployeeContext _context;

        public DeductionPoliciesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: DeductionPolicies
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeductionPolicy.ToListAsync());
        }

        // GET: DeductionPolicies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deductionPolicy = await _context.DeductionPolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deductionPolicy == null)
            {
                return NotFound();
            }

            return View(deductionPolicy);
        }

        // GET: DeductionPolicies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeductionPolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Amount,Date")] DeductionPolicy deductionPolicy)
        {
            if (ModelState.IsValid)
            {
                deductionPolicy.Date = DateTime.Today;
                _context.Add(deductionPolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deductionPolicy);
        }

        // GET: DeductionPolicies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deductionPolicy = await _context.DeductionPolicy.FindAsync(id);
            if (deductionPolicy == null)
            {
                return NotFound();
            }
            return View(deductionPolicy);
        }

        // POST: DeductionPolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Amount,Date")] DeductionPolicy deductionPolicy)
        {
            if (id != deductionPolicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deductionPolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeductionPolicyExists(deductionPolicy.Id))
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
            return View(deductionPolicy);
        }

        // GET: DeductionPolicies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deductionPolicy = await _context.DeductionPolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deductionPolicy == null)
            {
                return NotFound();
            }

            return View(deductionPolicy);
        }

        // POST: DeductionPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deductionPolicy = await _context.DeductionPolicy.FindAsync(id);
            _context.DeductionPolicy.Remove(deductionPolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeductionPolicyExists(int id)
        {
            return _context.DeductionPolicy.Any(e => e.Id == id);
        }
    }
}
