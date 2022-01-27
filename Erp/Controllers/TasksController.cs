using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Erp.Data;
using Erp.Models;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Erp.Controllers
{
    public class TasksController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public TasksController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Director, ProjectManager")]
        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var task = _context.Taskss.Include(t => t.Project);
            return View(await task.ToListAsync());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> IndexEmp()
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            var task = _context.Assigns.Where(q => q.EmpolyeeId == emp.Id).Include(w=>w.Tasks).ToList();
         
            return View(task);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader")]
        public async Task<IActionResult> IndexT()
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            var task = _context.Assigns.Where(q=>q.FromEmp == emp.Id).Include(t => t.Tasks);
            return View(await task.ToListAsync());
        }
      
        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Taskss
                .Include(t => t.Project)
                 .Include(t => t.Assign.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader, ProjectManager")]
        // GET: Tasks/Create
        public async Task<IActionResult> Create()
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            ViewData["ProjectId"] = new SelectList(_context.Project.Where(e=>e.EmployeeId == emp.Id), "Id", "ProjectName");
            return View();

        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskName,Duration,Description,TaskCost,TaskProgress,PercentCoverage,Status,ProjectId")] Tasks tasks)
        {

            if (tasks == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                tasks.Status = null;

                _context.Add(tasks);
                await _context.SaveChangesAsync();
                TempData["Success"] = "You have inserted task successfully";

            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", tasks.ProjectId);

            return RedirectToAction(nameof(Create));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Assign(int id)
        {
            User users = await _userManager.GetUserAsync(User);
            var teamleader = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            var employeeCode = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var emp = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employeeCode);

            if (emp == null)
            {
                TempData["Warning"] = "You have Inserted Invalid Employee Id";
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            else
            {
                if (emp.DepartmentId == teamleader.DepartmentId)
                {
                    var assigned = _context.Assigns.FirstOrDefault(e => e.TaskId == id);
                    var tasks = _context.Taskss.FirstOrDefault(q => q.Id == id);
                    

                    if (assigned == null)
                    {
                        
                        Assign assigns = new Assign();
                        assigns.EmpolyeeId = emp.Id;
                        assigns.FromEmp = teamleader.Id;
                        assigns.ProjectId = tasks.ProjectId;
                        assigns.TaskId = id;
                        _context.Add(assigns);
                        await _context.SaveChangesAsync();


                        tasks.AssignId = assigns.Id;
                        _context.Update(tasks);
                        await _context.SaveChangesAsync();

                        TempData["Success"] = "You have Successfully Assigned Task";
                        return RedirectToAction(nameof(Edit), new { id = id });

                    }
                    else 
                    {
                        var mayass = assigned.EmpolyeeId == emp.Id;

                        if (mayass)
                        {
                            TempData["Warning"] = "You have Already Assigned Task";
                            return RedirectToAction(nameof(Edit), new { id = id });
                        }
                        else
                        {
                            assigned.EmpolyeeId = emp.Id;

                            _context.Update(assigned);
                            await _context.SaveChangesAsync();

                            TempData["Success"] = "You have Successfully Assigned Task";
                            return RedirectToAction(nameof(Edit), new { id = id });
                        }
                           
                    }
                }
                else
                {
                    TempData["Warning"] = "You have Inserted Employee from another Department";
                    return RedirectToAction(nameof(Edit), new { id = id });
                
                }
                return RedirectToAction(nameof(Edit), new { id = id });
            }
           
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditEmp(int? id)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        // POST: Tasks/Edit/5
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
                    if (!TasksExists(tasks.Id))
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

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmp(int id, [Bind("Id,TaskName,Duration,Description,TaskCost,TaskProgress,PercentCoverage,Status,ProjectId")] Tasks tasks)
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
                    if (!TasksExists(tasks.Id))
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Taskss
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Taskss.FindAsync(id);
            var assigns =  _context.Assigns.FirstOrDefault(q=>q.TaskId == tasks.Id);
          
            if(assigns != null)
            {
                _context.Assigns.Remove(assigns);
                await _context.SaveChangesAsync();
            }

            _context.Taskss.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Taskss.Any(e => e.Id == id);
        }

    }
}