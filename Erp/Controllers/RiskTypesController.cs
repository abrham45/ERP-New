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
    public class RiskTypesController : Controller
    {
        private readonly EmployeeContext _context;

        public RiskTypesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: RiskTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RiskType.ToListAsync());
        }

        // GET: RiskTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskType = await _context.RiskType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riskType == null)
            {
                return NotFound();
            }

            return View(riskType);
        }

        // GET: RiskTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RiskTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] RiskType riskType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riskType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(riskType);
        }

        // GET: RiskTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskType = await _context.RiskType.FindAsync(id);
            if (riskType == null)
            {
                return NotFound();
            }
            return View(riskType);
        }

        // POST: RiskTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] RiskType riskType)
        {
            if (id != riskType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riskType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiskTypeExists(riskType.Id))
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
            return View(riskType);
        }

        // GET: RiskTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskType = await _context.RiskType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riskType == null)
            {
                return NotFound();
            }

            return View(riskType);
        }

        // POST: RiskTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var riskType = await _context.RiskType.FindAsync(id);
            _context.RiskType.Remove(riskType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiskTypeExists(int id)
        {
            return _context.RiskType.Any(e => e.Id == id);
        }
    }
}
