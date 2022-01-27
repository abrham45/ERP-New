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
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Erp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _usermanager;

        public ProjectsController(EmployeeContext context, UserManager<User> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }
       
        [Authorize(Roles = "Director, TeamLeader")]
        public async Task<IActionResult> ExportToExcel()
        {
            var users = _usermanager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users);

            if(emp != null)
            {
                var proj = from a in _context.Project.Where(e => e.DirectorId == emp.Id) select a;
                var Projects = from a in _context.Project.Where(e => e.EmployeeId == emp.Id) select a;

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Projects");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "ID";
                    worksheet.Cell(currentRow, 2).Value = "Project Name";
                    worksheet.Cell(currentRow, 3).Value = "Project Description";
                    worksheet.Cell(currentRow, 4).Value = "Project Budget";
                    worksheet.Cell(currentRow, 5).Value = "Project Duration";
                    worksheet.Cell(currentRow, 6).Value = "Project Start Date";
                    worksheet.Cell(currentRow, 7).Value = "Status";


                    if (proj != null)
                    {
                        foreach (var i in proj)
                        {
                            {
                                currentRow++;

                                worksheet.Cell(currentRow, 1).Value = i.Id;
                                worksheet.Cell(currentRow, 2).Value = i.ProjectName;
                                worksheet.Cell(currentRow, 3).Value = i.ProjectDescription;
                                worksheet.Cell(currentRow, 4).Value = i.ProjectBudget;
                                worksheet.Cell(currentRow, 5).Value = i.ProjectDuration;
                                worksheet.Cell(currentRow, 6).Value = i.ProjectStartDate;
                                worksheet.Cell(currentRow, 7).Value = i.Status;

                            }
                        }
                    }
                    else if (Projects != null)
                    {
                        foreach (var i in proj)
                        {
                            {
                                currentRow++;

                                worksheet.Cell(currentRow, 1).Value = i.Id;
                                worksheet.Cell(currentRow, 2).Value = i.ProjectName;
                                worksheet.Cell(currentRow, 3).Value = i.ProjectDescription;
                                worksheet.Cell(currentRow, 4).Value = i.ProjectBudget;
                                worksheet.Cell(currentRow, 5).Value = i.ProjectDuration;
                                worksheet.Cell(currentRow, 6).Value = i.ProjectStartDate;
                                worksheet.Cell(currentRow, 7).Value = i.Status;

                            }
                        }
                    }
                    else
                    {
                        TempData["Warning"] = "No projects Found.";
                        return RedirectToAction(nameof(Create));
                    }


                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        Response.Clear();
                        Response.Headers.Add("content-disposition", "attachment;filename=projects.xls");
                        Response.ContentType = "application/xls";
                        await Response.Body.WriteAsync(content);
                        Response.Body.Flush();
                    }

                    return View();


                }


            }
            else
            {
                TempData["Warning"] = "Fill in your detail first.";
                return RedirectToAction(nameof(Create));
            }



        }
        // GET: Employees/Details/5
        // POST: Experiences/Edit/5
        // GET: Projects
        [Authorize(Roles = "Director")]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
         
            return View(await PaginatedList<Project>.CreateAsync(_context.Project, pageNumber, 10));
        }
        // GET: Projects
        [Authorize(Roles="TeamLeader")]
        public async Task<IActionResult> IndexT(int pageNumber = 1)
        {
            var users = _usermanager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users);

            return View(await PaginatedList<Project>.CreateAsync(_context.Project.Where(e=>e.EmployeeId == emp.Id), pageNumber, 10));
        }
        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(q=>q.Employee)
                .Include(q => q.Employee.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectName,ProjectDescription,ProjectBudget,ProjectDuration,Status")] Project project)
        {
          
            var users = _usermanager.GetUserId(User);
            var dir = _context.Employees.FirstOrDefault(e => e.UserId == users);
            var employee = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);

            if (dir != null)
            {
                var emp = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employee);

                if (emp != null)
                {
                    if(emp.DivisionId == dir.Id)
                    {
                        project.EmployeeId = emp.Id;
                        project.DirectorId = dir.Id;
                        project.Status = false;
                        _context.Add(project);
                        await _context.SaveChangesAsync();
                        TempData["Success"] = "Project is Created and assigned Successfully";
                        return RedirectToAction(nameof(Create));
                    }
                    else
                    {
                        TempData["Warning"] = "Invalid Employee Id";
                        return RedirectToAction(nameof(Create));
                    }

                }
                else
                {
                    TempData["Warning"] = "Invalid Employee Id";
                    return RedirectToAction(nameof(Create));
                }
                   
            }
            else
            {
                TempData["Warning"] = "Please fill your information first";
                return RedirectToAction(nameof(Create));
            }
                
               
            
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project =  _context.Project
                .Include(e=>e.Employee)
                .FirstOrDefault(e=>e.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectName,ProjectDescription,ProjectBudget,ProjectDuration,Status")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var users = _usermanager.GetUserId(User);
                    var dir = _context.Employees.FirstOrDefault(e => e.UserId == users);
                    var employee = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);

                    if (dir != null)
                    {
                        var emp = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employee);

                        if (emp != null)
                        {
                            if (emp.DivisionId == dir.Id)
                            {
                                project.EmployeeId = emp.Id;
                                project.DirectorId = dir.Id;
                                _context.Update(project);
                                await _context.SaveChangesAsync();
                                TempData["Success"] = "Project is Updated Successfully";
                                return RedirectToAction(nameof(Create));
                            }
                            else
                            {
                                TempData["Warning"] = "Invalid Employee Id";
                                return RedirectToAction(nameof(Create));
                            }
                        }
                        else
                        {
                            TempData["Warning"] = "Invalid Employee Id";
                            return RedirectToAction(nameof(Create));
                        }
                    }
                    else
                    {
                        TempData["Warning"] = "Invalid Employee Id";
                        return RedirectToAction(nameof(Create));
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // POST: Asset_Exchange/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {

            var proj = await _context.Project.FindAsync(id);

            if (proj == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    proj.Status = true;
                    _context.Update(proj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(proj.Id))
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
        public async Task<IActionResult> Reject(int id)
        {
            var proj = await _context.Project.FindAsync(id);

            if (proj == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    proj.Status = false;
                    _context.Update(proj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(proj.Id))
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
        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
