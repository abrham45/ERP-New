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
    public class QualificationTypesController : Controller
    {
        private readonly EmployeeContext _context;

        public QualificationTypesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: QualificationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.QualificationType.ToListAsync());
        }

        // GET: QualificationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualificationType = await _context.QualificationType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qualificationType == null)
            {
                return NotFound();
            }

            return View(qualificationType);
        }

        // GET: QualificationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QualificationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,type")] QualificationType qualificationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qualificationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qualificationType);
        }

        // GET: QualificationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualificationType = await _context.QualificationType.FindAsync(id);
            if (qualificationType == null)
            {
                return NotFound();
            }
            return View(qualificationType);
        }

        // POST: QualificationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,type")] QualificationType qualificationType)
        {
            if (id != qualificationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qualificationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualificationTypeExists(qualificationType.Id))
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
            return View(qualificationType);
        }

        // GET: QualificationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualificationType = await _context.QualificationType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qualificationType == null)
            {
                return NotFound();
            }

            return View(qualificationType);
        }

        // POST: QualificationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qualificationType = await _context.QualificationType.FindAsync(id);
            _context.QualificationType.Remove(qualificationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QualificationTypeExists(int id)
        {
            return _context.QualificationType.Any(e => e.Id == id);
        }
    }
}
