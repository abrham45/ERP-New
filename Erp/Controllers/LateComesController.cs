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
    public class LateComesController : Controller
    {
        private readonly EmployeeContext _context;

        public LateComesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: LateComes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LateCome.ToListAsync());
        }

        // GET: LateComes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lateCome = await _context.LateCome
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lateCome == null)
            {
                return NotFound();
            }

            return View(lateCome);
        }

        // GET: LateComes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LateComes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MorningLate,AfternoonLate")] LateCome lateCome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lateCome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lateCome);
        }

        // GET: LateComes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lateCome = await _context.LateCome.FindAsync(id);
            if (lateCome == null)
            {
                return NotFound();
            }
            return View(lateCome);
        }

        // POST: LateComes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MorningLate,AfternoonLate")] LateCome lateCome)
        {
            if (id != lateCome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lateCome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LateComeExists(lateCome.Id))
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
            return View(lateCome);
        }

        // GET: LateComes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lateCome = await _context.LateCome
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lateCome == null)
            {
                return NotFound();
            }

            return View(lateCome);
        }

        // POST: LateComes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lateCome = await _context.LateCome.FindAsync(id);
            _context.LateCome.Remove(lateCome);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LateComeExists(int id)
        {
            return _context.LateCome.Any(e => e.Id == id);
        }
    }
}
