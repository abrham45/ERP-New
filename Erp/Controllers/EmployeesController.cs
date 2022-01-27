
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;
using PagedList;
using PagedList.Mvc;
using Erp.Repository;

namespace Erp.Controllers
{

    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public EmployeesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
     
        }
        /// <summary>
        /// //////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        /// 
        [Authorize(Roles = "HR-Admin")]
        public async Task<IActionResult> Index(string EmployeeCode, int pageNumber = 1)
        {


            var employees = from m in _context.Employees.OrderBy(e=>e.Team).Include(a => a.Team) select m;

            if (!String.IsNullOrEmpty(EmployeeCode))
            {
                employees = employees.Where(s => s.EmployeeCode.Contains(EmployeeCode));
            }
            return View(await PaginatedList<Employee>.CreateAsync(employees, pageNumber, 10));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> IndexSup(string EmployeeCode, int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

            var employees = _context.Employees.Where(a => a.TeamId == emp.TeamId).Where(a => a.Id != emp.Id).Include(a => a.Team);
           
            if (!String.IsNullOrEmpty(EmployeeCode))
            {
                employees = employees.Where(s => s.EmployeeCode.Contains(EmployeeCode)).Include(a => a.Team);
            }

            return View(await PaginatedList<Employee>.CreateAsync(employees, pageNumber, 10));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader")]
        public async Task<IActionResult> IndexT(string EmployeeCode, int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

            var employees = _context.Employees.Where(a => a.DepartmentId == emp.DepartmentId).Where(a => a.Id != emp.Id).Include(a => a.Team);
           
            if (!String.IsNullOrEmpty(EmployeeCode))
            {
                employees = employees.Where(s => s.EmployeeCode.Contains(EmployeeCode)).Include(a => a.Team);
            }

            return View(await PaginatedList<Employee>.CreateAsync(employees, pageNumber, 10));
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        [Authorize(Roles = "Director")]
        public async Task<IActionResult> IndexDir(string EmployeeCode, int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);
            var employees = _context.Employees.Where(a => a.DivisionId == emp.DivisionId).Include(e => e.Team).OrderByDescending(e=> e.EmployeeCode);

            if (!String.IsNullOrEmpty(EmployeeCode))
            {
                employees = employees.Where(s => s.EmployeeCode.Contains(EmployeeCode)).Include(e => e.Team).OrderByDescending(e => e.EmployeeCode);
            }

            return View(await PaginatedList<Employee>.CreateAsync(employees, pageNumber, 10));
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// 
        /// <returns></returns>
        /// 
        [Authorize(Roles = "HR-Admin, Director")]
        public async Task<IActionResult> ExportToExcel()
        {
            var emp = from a in _context.Employees select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "EmployeeCode";
                worksheet.Cell(currentRow, 3).Value = "FirstName";
                worksheet.Cell(currentRow, 4).Value = "LasttName";
                worksheet.Cell(currentRow, 5).Value = "Sex";
                worksheet.Cell(currentRow, 6).Value = "DOB";
                worksheet.Cell(currentRow, 7).Value = "Nationality";
                worksheet.Cell(currentRow, 8).Value = "Mobile";
                worksheet.Cell(currentRow, 9).Value = "City";
                worksheet.Cell(currentRow, 10).Value = "Region";
                worksheet.Cell(currentRow, 11).Value = "Woreda";
                worksheet.Cell(currentRow, 12).Value = "Department";
                worksheet.Cell(currentRow, 13).Value = "Team";
                worksheet.Cell(currentRow, 14).Value = "Position";
                worksheet.Cell(currentRow, 15).Value = "Employment Type";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.EmployeeCode;
                        worksheet.Cell(currentRow, 3).Value = i.FirstName;
                        worksheet.Cell(currentRow, 4).Value = i.LastName; ;
                        worksheet.Cell(currentRow, 5).Value = i.Sex;
                        worksheet.Cell(currentRow, 6).Value = i.DateOfBirth;
                        worksheet.Cell(currentRow, 7).Value = i.Nationality;
                        worksheet.Cell(currentRow, 8).Value = i.Mobile;
                        worksheet.Cell(currentRow, 9).Value = i.City;
                        worksheet.Cell(currentRow, 10).Value = i.Region;
                        worksheet.Cell(currentRow, 11).Value = i.Woreda;
                        worksheet.Cell(currentRow, 12).Value = i.Department.Name;
                        worksheet.Cell(currentRow, 13).Value = i.Team.Name;
                        worksheet.Cell(currentRow, 14).Value = i.Positions.Name;
                        worksheet.Cell(currentRow, 15).Value = i.Employment_Type.Name;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=employees.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();


            }
        }
        // GET: Employees/Details/5
        // POST: Experiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approved(int id)
        {

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    employee.Approve = true;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexDir));
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reject(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    employee.Approve = false;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexDir));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "HR-Admin, Supervisor, TeamLeader")]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.
                Include(e => e.Department).
                Include(e => e.Positions).
                Include(e => e.Employment_Type).
                Include(e => e.User).
                Include(e => e.Division).FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }


            return View(employee);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DetailDir(int? id)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Where(a => a.DivisionId == emp.DivisionId).
                Include(e => e.Department).
                Include(e => e.Positions).
                Include(e => e.Employment_Type).
                Include(e => e.User).
                

                Include(e => e.Division).FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }


            return View(employee);
        }

        // GET: Employees/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> Create()
        {
            var users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (_context.Employees != null)
            {
                if (emp != null)
                {
                    var employee = await _context.Employees.FindAsync(emp.Id);
                    ViewBag.position_id = new SelectList(_context.Position, "Id", "Name", employee.PositionId);
                    ViewBag.department_id = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
                    ViewBag.Employment_id = new SelectList(_context.Employment_Types, "Employment_TypeId", "Name", employee.Employment_typeId);
                    ViewBag.Division_id = new SelectList(_context.Division, "Id", "Name", employee.DivisionId);
                    ViewBag.Team_id = new SelectList(_context.Team, "Id", "Name", employee.TeamId);


                    return View(employee);
                }
                else
                {
                    ViewBag.position_id = new SelectList(_context.Position, "Id", "Name");
                    ViewBag.department_id = new SelectList(_context.Departments, "Id", "Name");
                    ViewBag.Employment_id = new SelectList(_context.Employment_Types, "Employment_TypeId", "Name");
                    ViewBag.Division_id = new SelectList(_context.Division, "Id", "Name");
                    ViewBag.Team_id = new SelectList(_context.Team, "Id", "Name");


                    return View();
                }
            }
            else
            {
               
                ViewBag.position_id = new SelectList(_context.Position, "Id", "Name");
                ViewBag.department_id = new SelectList(_context.Departments, "Id", "Name");
                ViewBag.Employment_id = new SelectList(_context.Employment_Types, "Employment_TypeId", "Name");
                ViewBag.Division_id = new SelectList(_context.Division, "Id", "Name");
                ViewBag.QualificationType = new SelectList(_context.QualificationType, "Id", "Name");
                ViewBag.Team_id = new SelectList(_context.Team, "Id", "Name");

                return View();
            }

        }
        /// <summary>
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateEmployee([Bind("EmployeeCode,FirstName,LastName,Sex,Nationality,DateOfBirth,Mobile,HomeTelephone,WorkTelephone,Fax,POBox,Country,Region,City,Subcity,Woreda,StartDate,EndDate,AboutMe,AreaOfExpertise,AccountNumber,DepartmentId,Employment_typeId,PositionId,TeamId,DivisionId,UserId,Field,Institution,QstartYear,QendYear,QualificationType,sName,sUrl,EName,EAddress,EPhoneNumber,ERelation")] Employee employee)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
            var uss = _context.Users.FirstOrDefault(e => e.Id == users.Id);


            if (emp != null)
            {


                emp.EmployeeCode = Convert.ToString(HttpContext.Request.Form["EmployeeCode"]);
                emp.FirstName = Convert.ToString(HttpContext.Request.Form["FirstName"]);
                emp.LastName = Convert.ToString(HttpContext.Request.Form["LastName"]);
                emp.Sex = Convert.ToString(HttpContext.Request.Form["Sex"]);
                emp.Nationality = Convert.ToString(HttpContext.Request.Form["Nationality"]);
                emp.DateOfBirth = Convert.ToDateTime(HttpContext.Request.Form["DateOfBirth"]);
                emp.Mobile = Convert.ToString(HttpContext.Request.Form["Mobile"]);
                emp.Country = Convert.ToString(HttpContext.Request.Form["Country"]);
                emp.Region = Convert.ToString(HttpContext.Request.Form["Region"]);
                emp.City = Convert.ToString(HttpContext.Request.Form["City"]);
                emp.Country = Convert.ToString(HttpContext.Request.Form["Country"]);
                emp.Subcity = Convert.ToString(HttpContext.Request.Form["Subcity"]);
                emp.Woreda = Convert.ToString(HttpContext.Request.Form["Woreda"]);
                emp.Woreda = Convert.ToString(HttpContext.Request.Form["Woreda"]);
            /*    emp.EndDate = Convert.ToDateTime(HttpContext.Request.Form["EndDate"]);*/
                emp.AboutMe = Convert.ToString(HttpContext.Request.Form["AboutMe"]);
                emp.AreaOfExpertise = Convert.ToString(HttpContext.Request.Form["AreaOfExpertise"]);
                emp.AccountNumber = Convert.ToInt16(HttpContext.Request.Form["AccountNumber"]);
                emp.StartDate = DateTime.Today;
                emp.DepartmentId = Convert.ToInt32(HttpContext.Request.Form["DepartmentId"]);
                emp.Employment_typeId = Convert.ToInt32(HttpContext.Request.Form["Employment_typeId"]);
                emp.PositionId = Convert.ToInt32(HttpContext.Request.Form["PositionId"]);
                emp.TeamId = Convert.ToInt32(HttpContext.Request.Form["TeamId"]);
                emp.DivisionId = Convert.ToInt32(HttpContext.Request.Form["DivisionId"]);

                emp.Field = Convert.ToString(HttpContext.Request.Form["Field"]);
                emp.Institution = Convert.ToString(HttpContext.Request.Form["Institution"]);
                emp.QstartYear = Convert.ToDateTime(HttpContext.Request.Form["QstartYear"]);
                emp.QendYear = Convert.ToDateTime(HttpContext.Request.Form["QendYear"]);
                emp.QualificationType = Convert.ToString(HttpContext.Request.Form["QualificationType"]);

                emp.sName = Convert.ToString(HttpContext.Request.Form["sName"]);
                emp.sUrl = Convert.ToString(HttpContext.Request.Form["sUrl"]);


                emp.EName = Convert.ToString(HttpContext.Request.Form["EName"]);
                emp.EAddress = Convert.ToString(HttpContext.Request.Form["EAddress"]);
                emp.EPhoneNumber = Convert.ToString(HttpContext.Request.Form["EPhoneNumber"]);
                emp.ERelation = Convert.ToString(HttpContext.Request.Form["ERelation"]);


                _context.Update(emp);
                await _context.SaveChangesAsync();


                uss.DepartmentId = emp.DepartmentId;
                uss.EmployeeId = emp.Id;

                _context.Update(uss);
                await _context.SaveChangesAsync();

                ViewBag.position_id = new SelectList(_context.Position, "PostionId", "Position", employee.PositionId);
                ViewBag.department_id = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
                ViewBag.Employment_id = new SelectList(_context.Employment_Types, "Employment_TypeId", "Name", employee.Employment_typeId);
                ViewBag.Division_id = new SelectList(_context.Division, "Id", "Name", employee.DivisionId);
                ViewBag.Team_id = new SelectList(_context.Division, "Id", "Name", employee.TeamId);

           

                return RedirectToAction(nameof(Create));

            }
            else
            {

                /* Qualification qual = new Qualification();
                 qual.Field = Convert.ToString(HttpContext.Request.Form["Field"]);
                 qual.Institution = Convert.ToString(HttpContext.Request.Form["Institution"]);
                 qual.StartYear = Convert.ToDateTime(HttpContext.Request.Form["StartYear"]);
                 qual.EndYear = Convert.ToDateTime(HttpContext.Request.Form["EndYear"]);
                 qual.QualificationType = Convert.ToString(HttpContext.Request.Form["QualificationType"]);

                 _context.Add(qual);
                 await _context.SaveChangesAsync();


                 Socials social = new Socials();
                 social.Name = Convert.ToString(HttpContext.Request.Form["Name"]);
                 social.Url = Convert.ToString(HttpContext.Request.Form["Url"]);


                 _context.Add(social);
                 await _context.SaveChangesAsync();

                 EmergencyContact emergency = new EmergencyContact();
                 emergency.Name = Convert.ToString(HttpContext.Request.Form["Name"]);
                 emergency.Address = Convert.ToString(HttpContext.Request.Form["Address"]);
                 emergency.PhoneNumber = Convert.ToString(HttpContext.Request.Form["PhoneNumber"]);
                 emergency.Relation = Convert.ToString(HttpContext.Request.Form["Relation"]);


                 _context.Add(emergency);
                 await _context.SaveChangesAsync();*/


                /*   var rand = new Random();
                   var EmployeeCode = rand.Next(100000, 999999);
                  employee.EmployeeCode = "INSA" + Convert.ToString(EmployeeCode);
                */


                employee.UserId = users.Id;
                employee.Status = true;
                employee.Approve = null;
                

                _context.Add(employee);
                await _context.SaveChangesAsync();


                uss.DepartmentId = employee.DepartmentId;
                uss.EmployeeId = employee.Id;

                _context.Update(uss);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Create));
            }

            
        }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            // GET: Employees/Delete/5

            [Authorize(Roles = "HR-Admin ,Basic")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}