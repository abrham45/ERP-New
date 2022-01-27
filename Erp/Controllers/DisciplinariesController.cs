using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;

namespace Erp.Controllers
{
    public class DisciplinariesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public DisciplinariesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Disciplinaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disciplinary.ToListAsync());
        }

        // GET: Disciplinaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinary = await _context.Disciplinary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplinary == null)
            {
                return NotFound();
            }

            return View(disciplinary);
        }

        // GET: Disciplinaries/Create
        public async Task<IActionResult> Create()
        {
            User users = await _userManager.GetUserAsync(User);
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.DisciplinaryId = new SelectList(_context.DisciplinaryType, "Id", "Type");
                return View();
            }
        }

        // POST: Disciplinaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,DisciplinaryTypeId,Description,Approved,Date,Detention")] Disciplinary disciplinary)
        {
            var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeCode == emp);
            disciplinary.EmployeeId = employee.Id;

            if(employee != null)
            {
                _context.Add(disciplinary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "Employee Doesn't Exisit!";
            }
            
            return View(disciplinary);
        }

        // GET: Disciplinaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinary = await _context.Disciplinary.FindAsync(id);
            if (disciplinary == null)
            {
                return NotFound();
            }
            return View(disciplinary);
        }

        // POST: Disciplinaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,DisciplinaryTypeId,Description,Approved,Date,Detention")] Disciplinary disciplinary)
        {
            if (id != disciplinary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplinary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaryExists(disciplinary.Id))
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
            return View(disciplinary);
        }

        // GET: Disciplinaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinary = await _context.Disciplinary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplinary == null)
            {
                return NotFound();
            }

            return View(disciplinary);
        }

        // POST: Disciplinaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplinary = await _context.Disciplinary.FindAsync(id);
            _context.Disciplinary.Remove(disciplinary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaryExists(int id)
        {
            return _context.Disciplinary.Any(e => e.Id == id);
        }
    }
}
