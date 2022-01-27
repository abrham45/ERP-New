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
    public class QualificationsController : Controller
    {
        private readonly EmployeeContext _context;

        public QualificationsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Qualifications
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Qualification.Include(q => q.QualificationType);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Qualifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualification
                .Include(q => q.QualificationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qualification == null)
            {
                return NotFound();
            }

            return View(qualification);
        }

        // GET: Qualifications/Create
        public IActionResult Create()
        {
            ViewData["QualificationTypeId"] = new SelectList(_context.Set<QualificationType>(), "Id", "Id");
            return View();
        }

        // POST: Qualifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Field,Institution,StartYear,EndYear,QualificationTypeId")] Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qualification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           // ViewData["QualificationTypeId"] = new SelectList(_context.Set<QualificationType>(), "Id", "Id", qualification.QualificationTypeId);
            return View(qualification);
        }

        // GET: Qualifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualification.FindAsync(id);
            if (qualification == null)
            {
                return NotFound();
            }
          //  ViewData["QualificationTypeId"] = new SelectList(_context.Set<QualificationType>(), "Id", "Id", qualification.QualificationTypeId);
            return View(qualification);
        }

        // POST: Qualifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Field,Institution,StartYear,EndYear,QualificationTypeId")] Qualification qualification)
        {
            if (id != qualification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qualification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualificationExists(qualification.Id))
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
          //  ViewData["QualificationTypeId"] = new SelectList(_context.Set<QualificationType>(), "Id", "Id", qualification.QualificationTypeId);
            return View(qualification);
        }

        // GET: Qualifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualification
                .Include(q => q.QualificationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qualification == null)
            {
                return NotFound();
            }

            return View(qualification);
        }

        // POST: Qualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qualification = await _context.Qualification.FindAsync(id);
            _context.Qualification.Remove(qualification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QualificationExists(int id)
        {
            return _context.Qualification.Any(e => e.Id == id);
        }
    }
}
