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
    public class hierarchicalsController : Controller
    {
        private readonly EmployeeContext _context;

        public hierarchicalsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: hierarchicals
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.hierarchical.Include(h => h.Parent);

            return View(await employeeContext.ToListAsync());
            //return View(await _context.Units.ToListAsync());
        }

        // GET: hierarchicals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarchical = await _context.hierarchical
                .Include(h => h.Parent)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hierarchical == null)
            {
                return NotFound();
            }

            return View(hierarchical);
        }

        // GET: hierarchicals/Create
        public IActionResult Create()
        {
            ViewData["Pid"] = new SelectList(_context.hierarchical, "ID", "ParentName");
            return View();
        }

        // POST: hierarchicals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Pid")] hierarchical hierarchical)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hierarchical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pid"] = new SelectList(_context.hierarchical, "ID", "Name", hierarchical.Name);
            return View(hierarchical);
        }

        // GET: hierarchicals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarchical = await _context.hierarchical.FindAsync(id);
            if (hierarchical == null)
            {
                return NotFound();
            }
            ViewData["Pid"] = new SelectList(_context.hierarchical, "ID", "Name", hierarchical.Name);
            return View(hierarchical);
        }

        // POST: hierarchicals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Pid")] hierarchical hierarchical)
        {
            if (id != hierarchical.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hierarchical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!hierarchicalExists(hierarchical.ID))
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
            ViewData["Pid"] = new SelectList(_context.hierarchical, "ID", "Name", hierarchical.Name);
            return View(hierarchical);
        }

        // GET: hierarchicals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hierarchical = await _context.hierarchical
                .Include(h => h.Parent)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hierarchical == null)
            {
                return NotFound();
            }

            return View(hierarchical);
        }

        // POST: hierarchicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hierarchical = await _context.hierarchical.FindAsync(id);
            _context.hierarchical.Remove(hierarchical);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool hierarchicalExists(int id)
        {
            return _context.hierarchical.Any(e => e.ID == id);
        }
    }
}
