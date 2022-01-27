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
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Erp.Controllers
{
    public class MaintenancesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public MaintenancesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
       

        // GET: Maintenances
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Maintenance.Include(m => m.Employee).Include(m => m.vechicles);
            return View(await employeeContext.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ExportToExcel()
        {
            var emp = from a in _context.Maintenance.Include(e=>e.Employee).Include(e => e.vechicles) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Maintenance");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Employee";
                worksheet.Cell(currentRow, 3).Value = "vechicles";
                worksheet.Cell(currentRow, 4).Value = "Status";
                worksheet.Cell(currentRow, 5).Value = "Feedback";
                worksheet.Cell(currentRow, 6).Value = "Description";
                worksheet.Cell(currentRow, 7).Value = "Date";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Employee.EmployeeCode;
                        worksheet.Cell(currentRow, 3).Value = i.vechicles.PlateNumber;
                        worksheet.Cell(currentRow, 4).Value = i.Status;
                        worksheet.Cell(currentRow, 5).Value = i.Feedback;
                        worksheet.Cell(currentRow, 6).Value = i.Description;
                        worksheet.Cell(currentRow, 7).Value = i.Date;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=Maintenance.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
        // GET: Maintenances
        public async Task<IActionResult> IndexUser()
        {
            var users = _userManager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users);

            var employeeContext = _context.Maintenance.Include(m => m.Employee).Include(m => m.vechicles).Where(e=>e.EmployeeId == emp.Id);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Maintenances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenance
                .Include(m => m.Employee)
                .Include(m => m.vechicles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // GET: Maintenances/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Maintenances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,vechiclesId,Status,Feedback,Date,Description,EmployeeId")] Maintenance maintenance)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(q => q.UserId == users.Id);

            if (emp != null)
            {
                var vechicleAll = _context.EmployeeAllocation_1.FirstOrDefault(q => q.EmployeeId == emp.Id);

                if (vechicleAll != null)
                {

                    maintenance.Date = DateTime.Today;
                    maintenance.Status = null;
                    maintenance.vechiclesId = vechicleAll.VehicleId;
                    maintenance.EmployeeId = emp.Id;

                    _context.Add(maintenance);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Vehicle is inserted to maintainance successfully";
                    return RedirectToAction(nameof(Create));

                }
                else
                {
                    TempData["Warning"] = "You Donot have a Vehicle yet.";
                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                TempData["Warning"] = "Please fill in your Detail.";
                return RedirectToAction(nameof(Create));
            }

            return View(maintenance);
        }

        // GET: Maintenances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenance.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }
           
            return View(maintenance);
        }

        // POST: Maintenances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,vechiclesId,Status,Feedback,Date,Description,EmployeeId")] Maintenance maintenance)
        {
            if (id != maintenance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceExists(maintenance.Id))
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
           
            return View(maintenance);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {

            var maintain = await _context.Maintenance.FindAsync(id);
            var feed = Convert.ToString(HttpContext.Request.Form["Feedback"]);

            if (maintain == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    maintain.Status = true;
                    maintain.Feedback = feed;
                    _context.Update(maintain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceExists(maintain.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details));
            }

        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reject(int id)
        {

            var maintain = await _context.Maintenance.FindAsync(id);
            var feed = Convert.ToString(HttpContext.Request.Form["Feedback"]);

            if (maintain == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    maintain.Status = false;
                    maintain.Feedback = feed;
                    _context.Update(maintain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceExists(maintain.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details));
            }

        }


        // GET: Maintenances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenance
                .Include(m => m.Employee)
                .Include(m => m.vechicles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // POST: Maintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenance = await _context.Maintenance.FindAsync(id);
            _context.Maintenance.Remove(maintenance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceExists(int id)
        {
            return _context.Maintenance.Any(e => e.Id == id);
        }
    }
}
