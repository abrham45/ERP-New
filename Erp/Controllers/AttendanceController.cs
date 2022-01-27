using Erp.Areas.Identity.Data;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Erp.Areas.Identity.Pages.Account.LoginModel;

namespace Erp.Controllers
{
    public class AttendanceController : Controller
    {

        // GET: AttendanceController
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public InputModel Input { get; set; }
        public AttendanceController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "HR-Admin, Director")]
        // GET: Attendances
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
          /*  return View(await _context.Attendance
                .Where(e=>e.Date == DateTime.Today)
                .Include(a => a.Employee).OrderByDescending(e=>e.Date).ToListAsync());*/

            return View(await PaginatedList<Attendances>.CreateAsync(_context.Attendance.Where(e => e.Date == DateTime.Today)
                .Include(a => a.Employee).OrderByDescending(e => e.Date), pageNumber, 7));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<IActionResult> IndexBasicI(int pageNumber = 1)
        {

            var user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

            if (emp != null)
            {
                var incomplete = _context.Attendance.Include(p => p.Employee)
                    .Where(e => e.MorningCheckin == TimeSpan.Zero |
                    e.MorningCheckout == TimeSpan.Zero |
                    e.AfternoonCheckin == TimeSpan.Zero |
                    e.AfternoonCheckout == TimeSpan.Zero);
                return View(await incomplete.ToListAsync());
            }
            else
            {
                TempData["Warning"] = "You can not create excuses.";
                return RedirectToAction(nameof(IndexBasic));
            }
        }
        /// <summary>
        /// / 
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> IndexBasic(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emp != null)
            {

                var att = _context.Attendance.Where(e => e.Date == DateTime.Today)
                    .Where(e => e.EmployeeId == emp.Id).Include(e => e.Employee).OrderByDescending(e => e.Date);

                return View(await PaginatedList<Attendances>.CreateAsync(att, pageNumber, 7));
 
            }
            else
            {
                return NotFound();
            }
         
        }
        /// <summary>
        /// /
        /// 
        /// </summary>
     
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader")]
        public async Task<IActionResult> IndexTeam(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            var usesrss = _context.Employees.Where(a => a.DepartmentId == emp.DepartmentId).Select(e => e.Id).ToList();

            if (usesrss != null)
            {
                var att = _context.Attendance.Include(e => e.Employee)
                    .Where(e => e.Date == DateTime.Today)
                    .Where(e => usesrss.Contains(e.EmployeeId))
                    .OrderByDescending(e => e.Date);

                return View(await PaginatedList<Attendances>.CreateAsync(att, pageNumber, 7));
           
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> IndexSup(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
                   
            var usesrss = _context.Employees.Where(e => e.PositionId != 2).Where(a => a.TeamId == emp.TeamId).Select(e=>e.Id).ToList();

            if (usesrss != null)
            {

                var atts = _context.Attendance
                    .Include(e => e.Employee)
                    .Where(e => e.Date == DateTime.Today)
                    .Where(e => usesrss.Contains(e.EmployeeId))
                    .OrderByDescending(e => e.Date);
                    

                return View(await PaginatedList<Attendances>.CreateAsync(atts, pageNumber, 7));

            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "HR-Admin, Director")]
        // GET: AttendanceController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance
                            .Include(e=>e.Employee)
                            .Include(e=>e.Employee.User)
                            .Where(m => m.EmployeeId == id)
                            .ToListAsync();
         

            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Detail(int? id)
        {

            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            var profilePicture = emp.User.ProfilePicture;

            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance.Include(e => e.Employee)
                                .Where(m => m.EmployeeId == id).ToListAsync();
            
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);

        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DetailsSup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance
                                .Include(e => e.Employee)
                                .Where(m => m.EmployeeId == id)
                                .ToListAsync();

            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);

        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            /*var userss = await _userManager.GetUserAsync(HttpContext.User);*/
            var users =  _userManager.GetUserId(HttpContext.User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == users);
            var attend = _context.Attendance.Include(a=>a.LateCome).AsEnumerable().LastOrDefault(e => e.EmployeeId == emp.Id & e.Date == DateTime.Today);
            ViewData["LateCome"] = _context.LateCome.FirstOrDefault();
            ViewData["LeaveRequest"] = _context.LeaveRequests.Where(a=>a.Approved==true).AsEnumerable().LastOrDefault(a => a.EmployeeId == emp.Id);
            return View(attend);
        }

        // GET: AttendanceController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SaveAttendance()
        {
            User users = await _userManager.GetUserAsync(User);
            /*  var users = _userManager.GetUserId(HttpContext.User);*/
            var employee_codes = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);

            var emp = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employee_codes);
            if(emp != null)
            {
                var check = emp.UserId == users.Id;
                var empAttend = _context.Attendance.Include(a => a.LateCome).AsEnumerable().LastOrDefault(e => e.EmployeeId == emp.Id);

                TimeSpan start = new TimeSpan(6, 20, 0); //12 : 20
                TimeSpan end = new TimeSpan(6, 45, 0); //12 :45
                TimeSpan now = DateTime.Now.TimeOfDay;
                TimeSpan nstart = new TimeSpan(07, 30, 0);

                if (check)
                {
                    if (empAttend != null)
                    {
                        if (empAttend.Date != DateTime.Today)
                        {

                            Attendances amodel = new Attendances();

                            if (now > nstart)
                            {
                                amodel.AfternoonCheckin = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                                amodel.Date = DateTime.Today;
                                amodel.EmployeeId = emp.Id;

                                _context.Add(amodel);
                                _context.SaveChanges();

                                TempData["Success"] = "AfternoonCheckin";
                                TempData["SuccessTime"] = DateTime.Now.ToString("D");
                                TempData["SuccessDate"] = DateTime.Now;
                                return RedirectToAction(nameof(Create));
                            }
                            else if (now < end)
                            {
                                amodel.MorningCheckin = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                                amodel.Date = DateTime.Today;
                                amodel.EmployeeId = emp.Id;

                                _context.Add(amodel);
                                _context.SaveChanges();

                                TempData["Success"] = "Morning Checked In";
                                TempData["SuccessTime"] = DateTime.Now.ToString("D");
                                TempData["SuccessDate"] = DateTime.Now;
                                return RedirectToAction(nameof(Create));
                            }
                        }
                        else
                        {

                            if (empAttend.MorningCheckin != TimeSpan.Zero & empAttend.MorningCheckout == TimeSpan.Zero & now < end)
                            {

                                empAttend.MorningCheckout = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));

                                var MWH = new TimeSpan(Math.Abs(empAttend.MorningCheckout.Ticks - empAttend.MorningCheckin.Ticks));
                                empAttend.MorningWorkingHour = MWH;



                                _context.Update(empAttend);
                                _context.SaveChanges();

                                TempData["Success"] = "Morning Checked Out";
                                TempData["SuccessTime"] = DateTime.Now.ToString("D");
                                TempData["SuccessDate"] = DateTime.Now;
                                return RedirectToAction(nameof(Create));
                            }
                            else if (empAttend.AfternoonCheckin == TimeSpan.Zero & now > nstart)
                            {

                                empAttend.AfternoonCheckin = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));

                                _context.Update(empAttend);
                                _context.SaveChanges();

                                TempData["Success"] = "Afternoon Checked In";
                                TempData["SuccessTime"] = DateTime.Now.ToString("D");
                                TempData["SuccessDate"] = DateTime.Now;
                                return RedirectToAction(nameof(Create));

                            }
                            else if (empAttend.AfternoonCheckin != TimeSpan.Zero & empAttend.AfternoonCheckout == TimeSpan.Zero)
                            {


                                empAttend.AfternoonCheckout = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));

                                var AWh = new TimeSpan(Math.Abs(empAttend.AfternoonCheckout.Ticks - empAttend.AfternoonCheckin.Ticks));
                                empAttend.AfternoonWorkingHour = AWh;

                                var timework = new TimeSpan(Math.Abs(empAttend.MorningCheckout.Ticks - empAttend.MorningCheckin.Ticks) + Math.Abs(empAttend.AfternoonCheckout.Ticks - empAttend.AfternoonCheckin.Ticks));

                                empAttend.WorkHour = timework;


                                _context.Update(empAttend);
                                _context.SaveChanges();
                                TempData["Success"] = "Afternoon Checked Out";
                                TempData["SuccessTime"] = DateTime.Now.ToString("D");
                                TempData["SuccessDate"] = DateTime.Now;
                                return RedirectToAction(nameof(Create));

                            }

                        }
                    }
                    else
                    {
                        Attendances amodel = new Attendances();
                        amodel.MorningCheckin = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                        amodel.Date = DateTime.Today;
                        amodel.EmployeeId = emp.Id;

                        _context.Add(amodel);
                        _context.SaveChanges();

                        TempData["Success"] = "Morning Checked In";
                        TempData["SuccessTime"] = DateTime.Now.ToString("D");
                        TempData["SuccessDate"] = DateTime.Now;
                        return RedirectToAction(nameof(Create));
                    }
                    if (empAttend.MorningCheckin > empAttend.LateCome.MorningLate)
                    {
                        empAttend.Status = "late";
                    }
                    else if (empAttend.MorningCheckin == TimeSpan.Zero & empAttend.MorningCheckout == TimeSpan.Zero & empAttend.AfternoonCheckin == TimeSpan.Zero & empAttend.AfternoonCheckout == TimeSpan.Zero)
                    {
                        empAttend.Status = "Absent";
                    }
                    else
                    {
                        empAttend.Status = "Present";
                    }

                }
                else
                {
                    TempData["Warning"] = "Please Enter Your Correct Employee Id!!!";
                    return RedirectToAction(nameof(Create));
                }


                TempData["Success"] = "Today's Attendance is Completed";
                return RedirectToAction(nameof(Create));

            }
            else
            {
                TempData["Warning"] = "Please Enter Your Correct Employee Id!!!";
                return RedirectToAction(nameof(Create));
            }
        }
        // GET: AttendanceController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            User users = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var attendance =  _context.Attendance.Include(e=>e.Employee).FirstOrDefault(e=>e.Id == id);
            if (attendance != null)
            {
                return View(attendance);
            }
            else
            {
                return NotFound();
            }
           
      
            return View(attendance);
        }

        // POST: AttendanceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MorningCheckin,MorningCheckout,AfternoonCheckin,AfternoonCheckout,EmployeeId")] Attendances attendance)
        {
      
            if (id != attendance.Id)
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
                    if (!AttendanceExists(attendance.Id))
                    {
                        throw;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(attendance);
        }

        private bool AttendanceExists(int id)
        {
            throw new NotImplementedException();
        }


        // GET: AttendanceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AttendanceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
