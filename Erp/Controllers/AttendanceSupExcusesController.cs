using ClosedXML.Excel;
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    public class AttendanceSupExcusesController : Controller
    {

           private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public AttendanceSupExcusesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
       
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int pageNumber = 1)
        {

            var user = await _userManager.GetUserAsync(User);
            var empSup = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

            var emp = _context.Employees
                .Where(e => e.PositionId != 2)
                .Where(e => e.TeamId == empSup.TeamId)
                .Select(x => x.Id).ToList();

            if(emp != null)
            {
                var AttendanceSupExcuses = _context.AttendanceSupExcuses
               .Include(p => p.Employee)
               .Include(p => p.AttendanceExcuses)
               .Where(e => emp.Contains(e.EmployeeId));

             /*   if (!String.IsNullOrEmpty(emps))
                {
                    AttendanceSupExcuses = AttendanceSupExcuses.Where(s => s.Employee.EmployeeCode.Contains(emps));
                }*/
                return View(await PaginatedList<AttendanceSupExcuses>.CreateAsync(AttendanceSupExcuses, pageNumber, 7));
            }
            else
            {
                TempData["Warning"] = "You can not create excuses.";
                return RedirectToAction(nameof(Index));
            }
        }
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> IndexBasic(int pageNumber = 1)
        {

            var user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

            if (emp != null)
            {
                var AttendanceSupExcuses = _context.AttendanceSupExcuses.Include(p => p.Employee).Include(p => p.AttendanceExcuses).Include(p => p.Attendances)
                    .Where(e => e.EmployeeId == emp.Id);

                return View(await PaginatedList<AttendanceSupExcuses>.CreateAsync(AttendanceSupExcuses, pageNumber, 7));
            }
            else
            {
                TempData["Warning"] = "Please fill in your information.";
                return RedirectToAction(nameof(Index));
            }
        }
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader")]
        public async Task<IActionResult> ExportToExcel()
        {
            var user = await _userManager.GetUserAsync(User);
            var empSup = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);
            var emps = _context.Employees.Where(e => e.DepartmentId == empSup.DepartmentId).Select(e=>e.Id).ToList();

            if (empSup != null)
            {
                var atendanceExcuses = from a in _context.AttendanceSupExcuses.Include(e => e.Employee).Include(e => e.AttendanceExcuses).Where(e=> emps.Contains(e.EmployeeId)) select a;

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("AttendanceExcuses");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "ID";
                    worksheet.Cell(currentRow, 2).Value = "Employee ";
                    worksheet.Cell(currentRow, 3).Value = "Date";
                    worksheet.Cell(currentRow, 4).Value = "Reason";
                    worksheet.Cell(currentRow, 5).Value = "AttendanceExcuses";

                    foreach (var i in atendanceExcuses)
                    {
                        {
                            currentRow++;

                            worksheet.Cell(currentRow, 1).Value = i.Id;
                            worksheet.Cell(currentRow, 2).Value = i.Employee.EmployeeCode;
                            worksheet.Cell(currentRow, 3).Value = i.Date;
                            worksheet.Cell(currentRow, 4).Value = i.Reason;
                            worksheet.Cell(currentRow, 5).Value = i.AttendanceExcuses.ExcuseName;

                        }
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        Response.Clear();
                        Response.Headers.Add("content-disposition", "attachment;filename=AttendanceExcuses.xls");
                        Response.ContentType = "application/xls";
                        await Response.Body.WriteAsync(content);
                        Response.Body.Flush();
                    }

                    return View();

                }
            }
            else
            {
                TempData["Warning"] = "please fill your information first.";
                return Redirect(nameof(IndexTeam));
            }
           
        }
        /// <summary>
        /// 
        /// 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> IndexTeam(string empid, int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var empSup = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

            var emp = _context.Employees.Where(e => e.DepartmentId == empSup.DepartmentId).Select(x => x.Id).ToList();


            var AttendanceSupExcuses = from a in _context.AttendanceSupExcuses
                                        .Include(p => p.Employee)
                                        .Include(p => p.AttendanceExcuses)
                                        .Where(e => emp.Contains(e.EmployeeId) & e.Status == true) select a;

            if (!String.IsNullOrEmpty(empid))
            {
                AttendanceSupExcuses = AttendanceSupExcuses.Where(e => e.Employee.EmployeeCode.Contains(empid));
            }
            return View(await PaginatedList<AttendanceSupExcuses>.CreateAsync(AttendanceSupExcuses, pageNumber, 7));
        }
        /// <summary>
        /// //
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AttendanceSupExcusess = await _context.AttendanceSupExcuses
                .Include(p => p.Employee)
                .Include(p => p.AttendanceExcuses)
                  .Include(p => p.Attendances)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AttendanceSupExcusess == null)
            {
                return NotFound();
            }

            return View(AttendanceSupExcusess);
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

            var assetLoan = await _context.AttendanceSupExcuses.FindAsync(id);

            if (assetLoan == null)
            {
                return NotFound();
            }
            ViewBag.AttendanceExcusesId = new SelectList(_context.AttendanceExcuses, "Id", "ExcuseName");
            return View(assetLoan);
        }
        /// <summary>
        /// //
        /// </summary>
        /// <param name="AttendanceSupExcusess"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,AttendanceExcusesId,Date,Status,Reason")] AttendanceSupExcuses AttendanceSupExcusess)
        {

            if (id != AttendanceSupExcusess.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(AttendanceSupExcusess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceSupExcusessExists(AttendanceSupExcusess.Id))
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
            
            ViewBag.AttendanceExcusesId = new SelectList(_context.AttendanceExcuses, "Id", "ExcuseName", AttendanceSupExcusess.AttendanceExcusesId);
            return View(AttendanceSupExcusess);
        }

        private bool AttendanceSupExcusessExists(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public IActionResult Create(int? id)
        {
            if( id != null)
            {

                ViewData["Attendance"] = _context.Attendance.Include(e=>e.Employee).FirstOrDefault(e=>e.Id == id);
                ViewBag.AttendanceExcusesId = new SelectList(_context.AttendanceExcuses, "Id", "ExcuseName");
                return View();
               
            }
            else
            {
                NotFound();
            }
           

            ViewBag.AttendanceExcusesId = new SelectList(_context.AttendanceExcuses, "Id", "ExcuseName");
            return View();
        }
        /// <summary>
        /// //
        /// </summary>
        /// <param name="AttendanceSupExcusess"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("EmployeeId,AttendanceExcusesId,Date,Status,Reason,AttendanceId")] AttendanceSupExcuses AttendanceSupExcusess)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);
            var exec = _context.AttendanceSupExcuses.FirstOrDefault(e => e.AttendanceId == id);

            if(exec == null)
            {
                if (emp != null)
                {
                    ViewBag.AttendanceExcusesId = new SelectList(_context.AttendanceExcuses, "Id", "ExcuseName", AttendanceSupExcusess.AttendanceExcusesId);
                    AttendanceSupExcusess.EmployeeId = emp.Id;
                    AttendanceSupExcusess.Status = null;
                    AttendanceSupExcusess.Date = DateTime.Today;
                    AttendanceSupExcusess.AttendanceId = id;

                    _context.Add(AttendanceSupExcusess);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.AttendanceExcusesId = new SelectList(_context.AttendanceExcuses, "Id", "ExcuseName", AttendanceSupExcusess.AttendanceExcusesId);
                    TempData["Warning"] = "Enter Valid Employee Id";
                    return RedirectToAction(nameof(Create));

                }
            }
            else
            {
               
                TempData["Warning"] = "Excuse Sent Already.";
                return RedirectToAction(nameof(Create));

            }


            return View(AttendanceSupExcusess);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<IActionResult> Approved(int id)
        {

            var AttendanceSupExcusess = await _context.AttendanceSupExcuses.FindAsync(id);
            if (AttendanceSupExcusess == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    AttendanceSupExcusess.Status = true;
                    _context.Update(AttendanceSupExcusess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (AttendanceSupExcusess != null)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reject(int id)
        {
            var AttendanceSupExcusess = await _context.AttendanceSupExcuses.FindAsync(id);

            if (AttendanceSupExcusess == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    AttendanceSupExcusess.Status = false;
                    _context.Update(AttendanceSupExcusess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (AttendanceSupExcusess != null)
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
        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.AttendanceSupExcuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.AttendanceSupExcuses.FindAsync(id);
            _context.AttendanceSupExcuses.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
            return _context.AttendanceSupExcuses.Any(e => e.Id == id);
        }
    }

}
