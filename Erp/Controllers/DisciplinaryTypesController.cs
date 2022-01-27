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
    public class DisciplinaryTypesController : Controller
    {
        private readonly EmployeeContext _context;

        public DisciplinaryTypesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: DisciplinaryTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DisciplinaryType.ToListAsync());
        }

        // GET: DisciplinaryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaryType = await _context.DisciplinaryType
                .FirstOrDefaultAsync(m => m.id == id);
            if (disciplinaryType == null)
            {
                return NotFound();
            }

            return View(disciplinaryType);
        }

        // GET: DisciplinaryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisciplinaryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,type,Describtion")] DisciplinaryType disciplinaryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplinaryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disciplinaryType);
        }

        // GET: DisciplinaryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaryType = await _context.DisciplinaryType.FindAsync(id);
            if (disciplinaryType == null)
            {
                return NotFound();
            }
            return View(disciplinaryType);
        }

        // POST: DisciplinaryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,type,Describtion")] DisciplinaryType disciplinaryType)
        {
            if (id != disciplinaryType.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplinaryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaryTypeExists(disciplinaryType.id))
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
            return View(disciplinaryType);
        }

        // GET: DisciplinaryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaryType = await _context.DisciplinaryType
                .FirstOrDefaultAsync(m => m.id == id);
            if (disciplinaryType == null)
            {
                return NotFound();
            }

            return View(disciplinaryType);
        }

        // POST: DisciplinaryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplinaryType = await _context.DisciplinaryType.FindAsync(id);
            _context.DisciplinaryType.Remove(disciplinaryType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaryTypeExists(int id)
        {
            return _context.DisciplinaryType.Any(e => e.id == id);
        }
    }
}
