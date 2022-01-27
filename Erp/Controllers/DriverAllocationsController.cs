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
    public class DriverAllocationsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public DriverAllocationsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "FleetTeam")]
        // GET: DriverAllocations
        [Authorize(Roles="FleetTeam")]
        public async Task<IActionResult> Index(string? driver)
        {
            var driverAllocation = _context.DriverAllocation.Include(d => d.Route).Include(d => d.Driver).Include(d => d.Vehicle);

            /*if (!String.IsNullOrEmpty(driver))
            {
                var driv = _context.Driver.Where(e=>e.Drivdriver);

                driverAllocation = driverAllocation.Where(s => s.Driver.DriverId.Contains(driver));
            }*/

            return View(await driverAllocation.ToListAsync());
           
        }
        [Authorize(Roles = "Driver")]
        // GET: DriverAllocations
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> IndexUser()
        {
            User users = await _userManager.GetUserAsync(User);
            var driverss = _context.Driver.FirstOrDefault(q => q.UserId == users.Id);

            var employeeContext = _context.DriverAllocation
                .Include(d => d.Driver)
                .Include(d => d.Vehicle)
                .Include(d => d.Route)
                .FirstOrDefault(q=>q.DriverId == driverss.Id);

            return View( employeeContext);
        }
        // GET: DriverAllocations/Details/5
        [Authorize(Roles = "FleetTeam")]
        public async Task<IActionResult> ExportToExcel()
        {
            var driverAllocations = from a in _context.DriverAllocation.Include(e=>e.Route).Include(e=>e.Vehicle).Include(e=>e.Driver) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Route");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Driver ";
                worksheet.Cell(currentRow, 3).Value = "Route";
                worksheet.Cell(currentRow, 4).Value = "Vehicle";

                foreach (var i in driverAllocations)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Driver.DriverId;
                        worksheet.Cell(currentRow, 3).Value = i.Route.Destination;
                        worksheet.Cell(currentRow, 4).Value = i.Vehicle.PlateNumber;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=AllocatedDrivers.xls");
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverAllocation = await _context.DriverAllocation
                .Include(d => d.Driver)
                .Include(d => d.Vehicle)
                 .Include(d => d.Route)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverAllocation == null)
            {
                return NotFound();
            }

            return View(driverAllocation);
        }

        [Authorize(Roles = "FleetTeam")]
        /*    Destination*/
        // GET: DriverAllocations/Create
        public IActionResult Create()
        {
            var RouteList = _context.Route.Select(x => new { Id = x.Id, RouteName = ( x.StartPoint + " " + x.Destination).ToString() });
            ViewBag.RouteId = new SelectList(RouteList, "Id", "RouteName");

            /*ViewBag.RouteId = new SelectList(_context.Route, "Id", "StartPoint");*/
            return View();
        }

        // POST: DriverAllocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "FleetTeam")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DriverId,VehicleId,RouteId")] DriverAllocation driverAllocation)
        {
            var RouteList = _context.Route.Select(x => new { Id = x.Id, RouteName = (x.StartPoint + " " + x.Destination).ToString() });

          
                var driver = Convert.ToString(HttpContext.Request.Form["DriverId"]);
                var vehicle = Convert.ToString(HttpContext.Request.Form["VehicleId"]);

                if (driver != null & vehicle != null)
                {
                    var valdriver = _context.Driver.FirstOrDefault(q => q.DriverId == driver);
                    var valvehicle = _context.vechicles.FirstOrDefault(q => q.PlateNumber == vehicle);

                    if (valdriver != null & valvehicle != null)
                    {
                   
                            var drivAll = _context.DriverAllocation.FirstOrDefault(e => e.DriverId == valdriver.Id & e.VehicleId == valvehicle.Id);

                            if (drivAll == null)
                            {
                                driverAllocation.DriverId = valdriver.Id;
                                driverAllocation.VehicleId = valvehicle.Id;

                                _context.Add(driverAllocation);
                                await _context.SaveChangesAsync();
                                TempData["Success"] = "Driver Allocated Successfully";
                                return RedirectToAction(nameof(Create));
                            }
                            else
                            {
                                TempData["Warning"] = "Already Allocated! Driver or Vehicle.";
                                return RedirectToAction(nameof(Create));
                            }

                        }
                        else
                        {
                    TempData["Warning"] = "Invalid Driver Id or Vehicle Plate Number";
                    ViewBag.RouteId = new SelectList(_context.Route, "Id", "StartPoint", driverAllocation.RouteId);
                    return RedirectToAction(nameof(Create));
                        }
                }
                else
                {
                    TempData["Warning"] = "Please Insert Driver Id and Vehicle Plate Number";
                    return RedirectToAction(nameof(Create));
                }

         
            ViewBag.RouteId = new SelectList(RouteList, "Id", "RouteName", driverAllocation.RouteId);
            
            return View(driverAllocation);


                
            
        }
        [Authorize(Roles = "FleetTeam")]
        // GET: DriverAllocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverAllocation = await _context.DriverAllocation.FindAsync(id);
            if (driverAllocation == null)
            {
                return NotFound();
            }
            var RouteList = _context.Route.Select(x => new { Id = x.Id, RouteName = (x.StartPoint + " " + x.Destination).ToString() });
            ViewBag.RouteId = new SelectList(RouteList, "Id", "RouteName", driverAllocation.RouteId);
            return View(driverAllocation);
        }
        [Authorize(Roles = "FleetTeam")]
        // POST: DriverAllocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DriverId,VehicleId,RouteId")] DriverAllocation driverAllocation)
        {
            if (id != driverAllocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var driver = Convert.ToString(HttpContext.Request.Form["DriverId"]);
                    var vehicle = Convert.ToString(HttpContext.Request.Form["VehicleId"]);

                    if (driver != null & vehicle != null)
                    {
                        var valdriver = _context.Driver.FirstOrDefault(q => q.DriverId == driver);
                        var valvehicle = _context.vechicles.FirstOrDefault(q => q.PlateNumber == vehicle);


                        if (valdriver != null & valvehicle != null)
                        {
                            driverAllocation.DriverId = valdriver.Id;
                            driverAllocation.VehicleId = valvehicle.Id;

                            _context.Update(driverAllocation);
                            await _context.SaveChangesAsync();
                            TempData["Success"] = "Driver Allocated Successfully";
                            return RedirectToAction(nameof(Edit));

                        }
                        else
                        {
                            TempData["Warning"] = "Invalid Driver Id or Vehicle Plate Number";
                            return RedirectToAction(nameof(Edit));
                        }
                    }
                    else
                    {
                        TempData["Warning"] = "Please Insert Driver Id and Vehicle Plate Number";
                        return RedirectToAction(nameof(Edit));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverAllocationExists(driverAllocation.Id))
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
            ViewBag.RouteId = new SelectList(_context.Route, "Id", "StartPoint", driverAllocation.RouteId);
            return View(driverAllocation);
        }
        [Authorize(Roles = "FleetTeam")]
        // GET: DriverAllocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverAllocation = await _context.DriverAllocation
                .Include(d => d.Driver)
                .Include(d => d.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverAllocation == null)
            {
                return NotFound();
            }

            return View(driverAllocation);
        }
        [Authorize(Roles = "FleetTeam")]
        // POST: DriverAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driverAllocation = await _context.DriverAllocation.FindAsync(id);
            _context.DriverAllocation.Remove(driverAllocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverAllocationExists(int id)
        {
            return _context.DriverAllocation.Any(e => e.Id == id);
        }
    }
}
