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
    public class EmployeeAllocationsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public EmployeeAllocationsController(EmployeeContext context , UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "FleetTeam")]
        // GET: EmployeeAllocations
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.EmployeeAllocation_1
                .Include(e => e.Employee)
                .Include(w => w.vechicles);
            return View(await employeeContext.ToListAsync());
        }
        [Authorize(Roles = "Director,Supervisor,TeamLeader")]
        public async Task<IActionResult> IndexEmployee()
        {

            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (assetal != null)
            {
                var empall = _context.EmployeeAllocation.Where(e => e.EmployeeId == assetal.Id).Include(e => e.Employee)
                    .Include(e => e.DriverAllocation.Route)
                    .Include(e => e.DriverAllocation.Vehicle);
             

                return View(empall);
            }
            else
            {
                return NotFound();
            }

        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "FleetTeam")]
        public async Task<IActionResult> ExportToExcel()
        {
            var empAllocations = from a in _context.EmployeeAllocation_1.Include(e => e.Employee).Include(e => e.vechicles) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("EmployeeVehicleAllocation");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Employee ";
                worksheet.Cell(currentRow, 4).Value = "Vehicle";
                worksheet.Cell(currentRow, 4).Value = "Vehicle Insurance";
                worksheet.Cell(currentRow, 5).Value = "Vehicle Model";

                foreach (var i in empAllocations)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Employee.EmployeeCode;
                        worksheet.Cell(currentRow, 3).Value = i.vechicles.PlateNumber;
                        worksheet.Cell(currentRow, 4).Value = i.vechicles.IsInsured;
                        worksheet.Cell(currentRow, 5).Value = i.vechicles.Model;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=employeeAllocations.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "FleetTeam")]

        // GET: EmployeeAllocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAllocation = await _context.EmployeeAllocation_1
                .Include(e => e.Employee)
                .Include(w => w.vechicles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAllocation == null)
            {
                return NotFound();
            }

            return View(employeeAllocation);
        }


        // GET: EmployeeAllocations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City");
            return View();
        }

        // POST: EmployeeAllocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleId,EmployeeId")] EmployeeAllocation employeeAllocation)
        {
            var employeeId = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var vehicleId = Convert.ToString(HttpContext.Request.Form["VehicleId"]);

                if (employeeId != null & vehicleId != null)
                {
                    var vehicle = _context.EmployeeAllocation.FirstOrDefault(q => q.DriverAllocation.Vehicle.PlateNumber == vehicleId);
                    var emp = _context.Employees.FirstOrDefault(q => q.EmployeeCode == employeeId);

                        if (vehicle != null & emp != null)
                        {
                    
                            employeeAllocation.VehicleId = vehicle.Id;
                            employeeAllocation.EmployeeId = emp.Id;

                            _context.Add(employeeAllocation);
                          await _context.SaveChangesAsync();
                        TempData["Success"] = "Employee service Allocation successful.";
                        return RedirectToAction(nameof(Create));
                                    }
                        else
                        {
                            TempData["Warning"] = "Invalid Employee Id or Vehicle Plate Number.";
                            return RedirectToAction(nameof(Create));
                        }
                }
                else
                {
                    TempData["Warning"] = "Invalid Employee Id or  Vehicle Plate Number.";
                    return RedirectToAction(nameof(Create));
                }
           
       /*     return View(employeeAllocation);*/
        }

        // GET: EmployeeAllocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAllocation = await _context.EmployeeAllocation_1.FindAsync(id);
            if (employeeAllocation == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City", employeeAllocation.EmployeeId);
            return View(employeeAllocation);
        }

        // POST: EmployeeAllocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleId,EmployeeId")] EmployeeAllocation employeeAllocation)
        {
            if (id != employeeAllocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeAllocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeAllocationExists(employeeAllocation.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City", employeeAllocation.EmployeeId);
            return View(employeeAllocation);
        }

        // GET: EmployeeAllocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAllocation = await _context.EmployeeAllocation_1
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAllocation == null)
            {
                return NotFound();
            }

            return View(employeeAllocation);
        }

        // POST: EmployeeAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeAllocation = await _context.EmployeeAllocation_1.FindAsync(id);
            _context.EmployeeAllocation_1.Remove(employeeAllocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAllocationExists(int id)
        {
            return _context.EmployeeAllocation_1.Any(e => e.Id == id);
        }
    }
}
