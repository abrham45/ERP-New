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
using Microsoft.AspNetCore.Authorization;

namespace Erp.Controllers
{
    public class DepartmentChangesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public DepartmentChangesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Director, TeamLeader")]
        // GET: DepartmentChanges
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.DepartmentChange.Include(d => d.Employee);
            return View(await employeeContext.ToListAsync());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
     /*   [Authorize(Roles = "Basic")]*/
        public async Task<IActionResult> IndexBasic()
        {
            User user = await _userManager.GetUserAsync(User);
            var Dc= _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (Dc  != null)
            {
                var empall = _context.DepartmentChange.Where(e => e.EmployeeId == Dc.Id);
                return View(await empall.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        // GET: DepartmentChanges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentChange = await _context.DepartmentChange
                .Include(d => d.User)
                .Include(e=>e.Employee)
                .Include(d=>d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentChange == null)
            {
                return NotFound();
            }

            return View(departmentChange);
        }
      /*  [Authorize(Roles = "Basic")]*/
        // GET: DepartmentChanges/Create
        public async Task<IActionResult> Create()
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
            if (emp == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.TransferTo = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: DepartmentChanges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentChange departmentchanges)
        {
            if (ModelState.IsValid)
            {

                User users = await _userManager.GetUserAsync(User);
                var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
                if (emp == null)
                {
                    return NotFound();
                }
                departmentchanges.EmployeeId = emp.Id;
                departmentchanges.from = emp.DepartmentId;
                departmentchanges.DepartmentId = Convert.ToInt32(HttpContext.Request.Form["DepartmentId"]);
                departmentchanges.reason = Convert.ToString(HttpContext.Request.Form["reason"]);
                departmentchanges.sentDate = DateTime.Today;
                departmentchanges.status = null;
                _context.Add(departmentchanges);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexBasic));
            }

            return View(departmentchanges);
        }

        // GET: DepartmentChanges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentChange = await _context.DepartmentChange.FindAsync(id);
            if (departmentChange == null)
            {
                return NotFound();
            }
            //    ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Country", departmentChange.EmployeeId);
            //   ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", departmentChange.UserId);
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.TransferTo = new SelectList(_context.Departments, "Id", "Name");
            return View(departmentChange);
        }

        // POST: DepartmentChanges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {

            var departmentChange = await _context.DepartmentChange.FindAsync(id);

            if (departmentChange == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    departmentChange.status = true;
                    _context.Update(departmentChange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentChangeExists(departmentChange.Id))
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

        }
        public async Task<IActionResult> Reject(int id)
        {

            var departmentChange = await _context.DepartmentChange.FindAsync(id);

            if (departmentChange == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    departmentChange.status = false;
                    _context.Update(departmentChange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentChangeExists(departmentChange.Id))
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

        }
        // GET: DepartmentChanges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentChange = await _context.Experience
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentChange == null)
            {
                return NotFound();
            }

            return View(departmentChange);
        }

        // POST: DepartmentChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentChange = await _context.DepartmentChange.FindAsync(id);
            _context.DepartmentChange.Remove(departmentChange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentChangeExists(int id)
        {
            return _context.DepartmentChange.Any(e => e.Id == id);
        }
    }
}
