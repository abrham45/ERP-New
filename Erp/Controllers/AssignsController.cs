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
    public class AssignsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public AssignsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Assigns
        public async Task<IActionResult> Index()
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            var task = _context.Assigns.Where(q => q.EmpolyeeId == emp.Id).Include(w => w.Tasks).ToList();

            return View(task);
        }

        // GET: Assigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assign = await _context.Assigns
                .Include(a => a.Tasks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assign == null)
            {
                return NotFound();
            }

            return View(assign);
        }

        // GET: Assigns/Create
        public IActionResult Create()
        {
            ViewData["TaskId"] = new SelectList(_context.Taskss, "Id", "TaskName");
            return View();
        }

        // POST: Assigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpolyeeId,TaskId,ProjectId,Status,FromEmp")] Assign assign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "TaskName", assign.TaskId);
            return View(assign);
        }

        // GET: Assigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Taskss.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", tasks.ProjectId);
            return View(tasks);
        }

        // POST: Assigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskName,Duration,Description,TaskCost,TaskProgress,PercentCoverage,Status,ProjectId")] Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "You have Updated Task successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignExists(tasks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        // GET: Assigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assign = await _context.Assigns
                .Include(a => a.Tasks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assign == null)
            {
                return NotFound();
            }

            return View(assign);
        }

        // POST: Assigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assign = await _context.Assigns.FindAsync(id);
            _context.Assigns.Remove(assign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignExists(int id)
        {
            return _context.Assigns.Any(e => e.Id == id);
        }
    }
}
