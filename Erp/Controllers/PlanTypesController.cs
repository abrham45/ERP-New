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
    public class PlanTypesController : Controller
    {
        private readonly EmployeeContext _context;

        public PlanTypesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: PlanTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlanType.ToListAsync());
        }

        // GET: PlanTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planType = await _context.PlanType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planType == null)
            {
                return NotFound();
            }

            return View(planType);
        }

        // GET: PlanTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PlanType planType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planType);
        }

        // GET: PlanTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planType = await _context.PlanType.FindAsync(id);
            if (planType == null)
            {
                return NotFound();
            }
            return View(planType);
        }

        // POST: PlanTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PlanType planType)
        {
            if (id != planType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanTypeExists(planType.Id))
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
            return View(planType);
        }

        // GET: PlanTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planType = await _context.PlanType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planType == null)
            {
                return NotFound();
            }

            return View(planType);
        }

        // POST: PlanTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planType = await _context.PlanType.FindAsync(id);
            _context.PlanType.Remove(planType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanTypeExists(int id)
        {
            return _context.PlanType.Any(e => e.Id == id);
        }
    }
}
