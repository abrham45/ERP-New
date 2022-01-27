using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Erp.Data;
using Erp.Models;
using Microsoft.EntityFrameworkCore;

namespace Erp.Controllers
{
    public class AttendanceExcusesController : Controller
    {
        private readonly EmployeeContext _context;

        public AttendanceExcusesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: AttendanceExcuses
        public async Task<IActionResult> Index()
        {
            var AttendanceExcuses = _context.AttendanceExcuses;
            return View(await AttendanceExcuses.ToListAsync());
        }
     
        // GET: AttendanceExcuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AttendanceExcuses = await _context.AttendanceExcuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AttendanceExcuses == null)
            {
                return NotFound();
            }

            return View(AttendanceExcuses);
        }

        // GET: AttendanceExcuses/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: AttendanceExcuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExcuseName,Description")] AttendanceExcuses AttendanceExcuses)
        {

            if (ModelState.IsValid)
            {

                _context.Add(AttendanceExcuses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(AttendanceExcuses);
        }

        // GET: AttendanceExcuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AttendanceExcuses = await _context.AttendanceExcuses.FindAsync(id);
            if (AttendanceExcuses == null)
            {
                return NotFound();
            }
            return View(AttendanceExcuses);
        }

        // POST: AttendanceExcuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExcuseName,Description")] AttendanceExcuses AttendanceExcuses)
        {
            if (id != AttendanceExcuses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(AttendanceExcuses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (AttendanceExcuses.Id == 0)
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
            return View(AttendanceExcuses);
        }
        // GET: AttendanceExcuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AttendanceExcuses = await _context.AttendanceExcuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AttendanceExcuses == null)
            {
                return NotFound();
            }
            return View(AttendanceExcuses);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var AttendanceExcusess = await _context.AttendanceExcuses.FindAsync(id);
            _context.AttendanceExcuses.Remove(AttendanceExcusess);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: AttendanceExcuses/Delete/5
    }
}