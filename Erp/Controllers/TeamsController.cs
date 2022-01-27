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
    public class TeamsController : Controller
    {
        private readonly EmployeeContext _context;

        public TeamsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Team.Include(p => p.Department);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Teams = await _context.Team
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Teams == null)
            {
                return NotFound();
            }

            return View(Teams);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DepartmentId")] Team Teams)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Teams);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name", Teams.DepartmentId);
            return View(Teams);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Teams = await _context.Team.FindAsync(id);
            if (Teams == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentId = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", Teams.DepartmentId);
            return View(Teams);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostionId,Team,DepartmentId")] Team Teams)
        {
            if (id != Teams.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Teams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsExists(Teams.Id))
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
            ViewBag.DepartmentId = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", Teams.DepartmentId);
            return View(Teams);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Teams = await _context.Team
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Teams == null)
            {
                return NotFound();
            }

            return View(Teams);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Teams = await _context.Team.FindAsync(id);
            _context.Team.Remove(Teams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(int id)
        {
            return _context.Team.Any(e => e.Id == id);
        }
    }
}
