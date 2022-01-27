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
    public class BonusPoliciesController : Controller
    {
        private readonly EmployeeContext _context;

        public BonusPoliciesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: BonusPolicies
        public async Task<IActionResult> Index()
        {
            return View(await _context.BonusPolicy.ToListAsync());
        }

        // GET: BonusPolicies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonusPolicy = await _context.BonusPolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bonusPolicy == null)
            {
                return NotFound();
            }

            return View(bonusPolicy);
        }

        // GET: BonusPolicies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BonusPolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Amount")] BonusPolicy bonusPolicy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bonusPolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bonusPolicy);
        }

        // GET: BonusPolicies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonusPolicy = await _context.BonusPolicy.FindAsync(id);
            if (bonusPolicy == null)
            {
                return NotFound();
            }
            return View(bonusPolicy);
        }

        // POST: BonusPolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Amount")] BonusPolicy bonusPolicy)
        {
            if (id != bonusPolicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bonusPolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BonusPolicyExists(bonusPolicy.Id))
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
            return View(bonusPolicy);
        }

        // GET: BonusPolicies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonusPolicy = await _context.BonusPolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bonusPolicy == null)
            {
                return NotFound();
            }

            return View(bonusPolicy);
        }

        // POST: BonusPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bonusPolicy = await _context.BonusPolicy.FindAsync(id);
            _context.BonusPolicy.Remove(bonusPolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BonusPolicyExists(int id)
        {
            return _context.BonusPolicy.Any(e => e.Id == id);
        }
    }
}
