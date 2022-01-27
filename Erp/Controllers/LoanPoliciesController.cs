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
    public class LoanPoliciesController : Controller
    {
        private readonly EmployeeContext _context;

        public LoanPoliciesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: LoanPolicies
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoanPolicy.ToListAsync());
        }

        // GET: LoanPolicies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanPolicy = await _context.LoanPolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanPolicy == null)
            {
                return NotFound();
            }

            return View(loanPolicy);
        }

        // GET: LoanPolicies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoanPolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,MaxAmount,Description,Date")] LoanPolicy loanPolicy)
        {
            if (ModelState.IsValid)
            {
                loanPolicy.Date = DateTime.Today;
                _context.Add(loanPolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loanPolicy);
        }

        // GET: LoanPolicies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanPolicy = await _context.LoanPolicy.FindAsync(id);
            if (loanPolicy == null)
            {
                return NotFound();
            }
            return View(loanPolicy);
        }

        // POST: LoanPolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,MaxAmount,Description,Date")] LoanPolicy loanPolicy)
        {
            if (id != loanPolicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanPolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanPolicyExists(loanPolicy.Id))
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
            return View(loanPolicy);
        }

        // GET: LoanPolicies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanPolicy = await _context.LoanPolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanPolicy == null)
            {
                return NotFound();
            }

            return View(loanPolicy);
        }

        // POST: LoanPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanPolicy = await _context.LoanPolicy.FindAsync(id);
            _context.LoanPolicy.Remove(loanPolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanPolicyExists(int id)
        {
            return _context.LoanPolicy.Any(e => e.Id == id);
        }
    }
}
