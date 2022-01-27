using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;

namespace Erp.Controllers
{

    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public AttendancesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
      /*  [Authorize(Roles = "HR-Admin, Director,TeamLeader")]*/
        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attendances.Include(a=> a.Employee).ToListAsync());
        }
       /* public async Task<IActionResult> Index(string attendance)
        {
            var employees = _context.Attendances.Include(a => a.Employee);

            if (!String.IsNullOrEmpty(attendance))
            {
                employees = employees.Where(s => s.EmployeeCode.Contains(attendance));
            }

            return View(await employees.ToListAsync());

        }*/
        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAttendance()
        {
            User user = await _userManager.GetUserAsync(User);
            var employees = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            var employee_codes = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var emp = _context.Employees.Where(a=>a.EmployeeCode == employees.EmployeeCode).FirstOrDefault(e => e.EmployeeCode == employee_codes);

            /*var employees = _context.Employees.Select(e => e.EmployeeCode);*/

            if (emp != null)
            {
                if (emp.Isin)
                {
                    var amodel = _context.Attendances.AsEnumerable().LastOrDefault(e => e.EmployeeId == emp.Id);
                    amodel.TimeOut = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                    _context.Update(amodel);
                }
                else
                {
                    Attendance amodel = new Attendance();
                    amodel.TimeIn = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                    amodel.Date = DateTime.Today;
                    amodel.EmployeeId = emp.Id;
                    _context.Add(amodel);
                }


                emp.Isin = !emp.Isin;
                _context.Update(emp);

                await _context.SaveChangesAsync();
                TempData["Success"] = emp.Isin ? "Checked In" : "Checked Out";
                TempData["SuccessTime"] = DateTime.Now.ToString("D");
                TempData["SuccessDate"] = DateTime.Now;
                return RedirectToAction(nameof(Create));
            }
            else
            {
                TempData["Warning"] = "Please Enter Your Employee Id!!!";
                return RedirectToAction(nameof(Create));
            }

            /* if (emp != null)
             {
                 if (emp.Isin)
                 {
                     var amodel = _context.Attendances.AsEnumerable().LastOrDefault(e => e.EmployeeId == emp.Id);
                     amodel.TimeOut = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                     _context.Update(amodel);
                 }
                 else
                 {
                     Attendance amodel = new Attendance();
                     amodel.TimeIn = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                     amodel.Date = DateTime.Today;
                     amodel.EmployeeId = emp.Id;
                     _context.Add(amodel);
                 }


                 emp.Isin = !emp.Isin;
                 _context.Update(emp);

                 await _context.SaveChangesAsync();
                 TempData["Success"] = emp.Isin ? "Checked In" : "Checked Out";
                 return RedirectToAction(nameof(Create));
             }
             else
             {
                 TempData["Warning"] = "Employee Doesn't Exist!!!";
                 return RedirectToAction(nameof(Create));
             }*/

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,employee_code,Date,Time")] Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
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
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
