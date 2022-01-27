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
    public class ServiceAllocationsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public ServiceAllocationsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: serviceAllocations
        [Authorize(Roles="FleetTeam")]
        public async Task<IActionResult> Index(string emps)
        {
            var employeeContext = _context.ServiceAllocation
                  .Include(e => e.Employee)
                .Include(e => e.DriverAllocation.Route)
                .Include(e => e.DriverAllocation)
                .Include(e => e.DriverAllocation.Driver)
                .Include(e => e.DriverAllocation.Vehicle);

            var user = await _userManager.GetUserAsync(User);
            var empSup = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

            var emp = _context.Employees
                .Where(e => e.PositionId != 2)
                .Where(e => e.TeamId == empSup.TeamId)
                .Select(x => x.Id).ToList();

            if (emp != null)
            {
                var AttendanceSupExcuses = _context.AttendanceSupExcuses
               .Include(p => p.Employee)
               .Include(p => p.AttendanceExcuses)
               .Where(e => emp.Contains(e.EmployeeId));

                /*    if (!String.IsNullOrEmpty(emps))
                {
                    employeeContext = employeeContext.Where(s => s.Employee.EmployeeCode.Contains(emps));
                }*/
            }
            return View(await employeeContext.ToListAsync());
        }
        // GET: serviceAllocations
        public async Task<IActionResult> IndexEmp()
        {
            User user = await _userManager.GetUserAsync(User);
            var emps = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emps != null)
            {
                var empall = _context.ServiceAllocation
                    .Include(e => e.DriverAllocation.Route)
                    .Include(e => e.DriverAllocation.Vehicle)
                    .Include(e => e.Employee)
                    .Include(e => e.DriverAllocation.Driver)
                    .FirstOrDefault(e => e.EmployeeId == emps.Id);
                if (empall != null)
                {
                    return View(empall);
                }
                else
                {
                    TempData["Warning"] = "Not allocated Allocated!";
                    return RedirectToAction(nameof(Create));
                }

            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
      
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: serviceAllocations
        public async Task<IActionResult> IndexUser()
        {
            User users = await _userManager.GetUserAsync(User);
            var driverss = _context.Driver.FirstOrDefault(q => q.UserId == users.Id);

            var employeeContext = _context.ServiceAllocation
                .Include(e => e.DriverAllocation.Route)
                .Include(e => e.DriverAllocation)
                .Include(e => e.Employee)
                .Where(q=>q.DriverAllocation.DriverId == driverss.Id);

            return View(employeeContext.ToList());
        }
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExportToExcel()
        {
            var empAllocations = from a in _context.ServiceAllocation.Include(e => e.Employee).Include(e => e.DriverAllocation.Driver).Include(e => e.DriverAllocation.Vehicle) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ServiceAllocation");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Employee ";
                worksheet.Cell(currentRow, 3).Value = "Vehicle";
                worksheet.Cell(currentRow, 4).Value = "Driver";

                foreach (var i in empAllocations)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Employee.EmployeeCode;
                        worksheet.Cell(currentRow, 3).Value = i.DriverAllocation.Vehicle.PlateNumber;
                        worksheet.Cell(currentRow, 4).Value = i.DriverAllocation.Driver.DriverId;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=serviceAllocations.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
       

        // GET: serviceAllocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceAllocation = await _context.ServiceAllocation
             .Include(e => e.DriverAllocation.Route)
                .Include(e => e.DriverAllocation)
                .Include(e => e.DriverAllocation.Driver)
                .Include(e => e.DriverAllocation.Vehicle)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceAllocation == null)
            {
                return NotFound();
            }
           

            return View(serviceAllocation);
        }

        // GET: serviceAllocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: serviceAllocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DriverAllocationId,EmployeeId")] ServiceAllocation serviceAllocation)
        {
           
                var employeeId = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
                var vehicleId = Convert.ToString(HttpContext.Request.Form["DriverAllocationId"]);

                if (employeeId != null & vehicleId != null)
                {
                    var vehicle = _context.DriverAllocation.FirstOrDefault(q => q.Vehicle.PlateNumber == vehicleId);
                    var emp = _context.Employees.FirstOrDefault(q => q.EmployeeCode == employeeId);


                    serviceAllocation.EmployeeId = emp.Id;

                 
                    if (vehicle != null & emp != null)
                    {
                        var vehicleall = _context.DriverAllocation.FirstOrDefault(q => q.VehicleId == vehicle.Id);

                        if(vehicleall != null)
                        {
                        var serviceAll = _context.ServiceAllocation
                            .Include(q => q.DriverAllocation)
                            .FirstOrDefault(q => q.DriverAllocation.VehicleId == vehicle.Id & q.EmployeeId == emp.Id);

                            if(serviceAll == null)
                            {
                                serviceAllocation.DriverAllocationId = vehicleall.Id;
                                serviceAllocation.EmployeeId = emp.Id;

                                _context.Add(serviceAllocation);
                                await _context.SaveChangesAsync();
                                TempData["Success"] = "Employee service Allocation successful.";
                                return RedirectToAction(nameof(Create));
                            }
                            else
                            {
                                TempData["Warning"] = "Employee Already Allocated!";
                                return RedirectToAction(nameof(Create));
                            }

                    }
                        else
                        {
                            TempData["Warning"] = "The Vehicle Inserted Isnot Allocated yet.";
                            return RedirectToAction(nameof(Create));
                        }
                       
                    }
                    else
                    {
                        TempData["Warning"] = "Invalid Employee Id or Driver Id.";
                        return RedirectToAction(nameof(Create));
                    }
                }
                else
                {
                    TempData["Warning"] = "Invalid Employee Id or Driver Id.";
                    return RedirectToAction(nameof(Create));
                }

            return View(serviceAllocation);
        }

        // GET: serviceAllocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceAllocation = await _context.ServiceAllocation.FindAsync(id);
            if (serviceAllocation == null)
            {
                return NotFound();
            }
            return View(serviceAllocation);
        }

        // POST: serviceAllocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DriverAllocationId,EmployeeId")] ServiceAllocation serviceAllocation)
        {
            if (id != serviceAllocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceAllocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!serviceAllocationExists(serviceAllocation.Id))
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
            return View(serviceAllocation);
        }

        // GET: serviceAllocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceAllocation = await _context.ServiceAllocation
                .Include(e => e.DriverAllocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceAllocation == null)
            {
                return NotFound();
            }

            return View(serviceAllocation);
        }

        // POST: serviceAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceAllocation = await _context.ServiceAllocation.FindAsync(id);
            _context.ServiceAllocation.Remove(serviceAllocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool serviceAllocationExists(int id)
        {
            return _context.ServiceAllocation.Any(e => e.Id == id);
        }
    }
}
