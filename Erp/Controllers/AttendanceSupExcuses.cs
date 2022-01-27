using Erp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    public class AttendanceSupExcuses : Controller
    {

           private readonly EmployeeContext _context;
        public AttendanceSupExcuses(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.AttendanceSupExcuses.Include(p => p.Employee);
            return View(await employeeContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AttendanceSupExcusess = await _context.AttendanceSupExcuses
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AttendanceSupExcusess == null)
            {
                return NotFound();
            }

            return View(AttendanceSupExcusess);
        }
        public IActionResult Create()
        {
            ViewBag.AttendanceExcusesId = new SelectList(_context.AttendanceExcuses, "Id", "ExcuseName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ? id)
        {
            var tasks = await _context.AttendanceSupExcuses.FindAsync(id);
            if (ModelState.IsValid)
            {
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewBag.AttendanceExcusesId = new SelectList(_context.AttendanceExcuses, "Id", "ExcuseName",tasks);
            return View(tasks);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.AttendanceSupExcuses.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", tasks.AttendanceExcusesId);
            return View(tasks);
        }
    }
}