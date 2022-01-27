using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Erp.Controllers
{
    public class DriversController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
       

        public DriversController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: Drivers
        [Authorize(Roles = "FleetTeam, Director")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Driver.ToListAsync());
        }
        // GET: Drivers
        public  IActionResult IndexUser()
        {
            var users =  _userManager.GetUserId(User);
            var driver =  _context.Driver.FirstOrDefault(q => q.UserId == users);

            return View(driver);
        }
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "FleetTeam")]
        public async Task<IActionResult> ExportToExcel()
        {
            var routes = from a in _context.Route select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Route");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "StartPoint";
                worksheet.Cell(currentRow, 3).Value = "Destination";
                worksheet.Cell(currentRow, 4).Value = "Expenses";

                foreach (var i in routes)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.StartPoint;
                        worksheet.Cell(currentRow, 3).Value = i.Destination;
                        worksheet.Cell(currentRow, 4).Value = i.Expenses;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=Route.xls");
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
        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

       
        // GET: Drivers/Create
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> Create()
        {
            var users =  _userManager.GetUserId(User);

            if (_context.Driver != null)
            {
                var driverss = _context.Driver.FirstOrDefault(q => q.UserId == users);
            /*    var drivers = _context.Driver.FirstOrDefault(q => q.Id == driverss.Id);*/
                
                if (driverss != null)
                {
                    return View(driverss);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
           
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDriver([Bind("Id,DriverId,FirstName,LastName,ExperienceInYears,Sex,Nationality,Date_of_birth,Mobile,Region,City,Subcity,Woreda,Status,StartDate,EndDate,LicenseNumber,ProfilePicture,License,UserId")] Driver driver)
        {
            User users = await _userManager.GetUserAsync(User);
            var driverss = _context.Driver.FirstOrDefault(e => e.UserId == users.Id);
            var uss = _context.Users.FirstOrDefault(e => e.Id == users.Id);

            if (driverss != null)
            {
                driverss.DriverId = Convert.ToString(HttpContext.Request.Form["DriverId"]);
                driverss.FirstName = Convert.ToString(HttpContext.Request.Form["FirstName"]);
                driverss.LastName = Convert.ToString(HttpContext.Request.Form["LastName"]);
                driverss.ExperienceInYears = Convert.ToInt32(HttpContext.Request.Form["ExperienceInYears"]);
                driverss.Sex = Convert.ToString(HttpContext.Request.Form["Sex"]);
                driverss.Nationality = Convert.ToString(HttpContext.Request.Form["Nationality"]);
                driverss.Date_of_birth = Convert.ToDateTime(HttpContext.Request.Form["Date_of_birth"]);
                driverss.Mobile = Convert.ToString(HttpContext.Request.Form["Mobile"]);
                driverss.Region = Convert.ToString(HttpContext.Request.Form["Region"]);
                driverss.City = Convert.ToString(HttpContext.Request.Form["City"]);
                driverss.Subcity = Convert.ToString(HttpContext.Request.Form["Subcity"]);
                driverss.Woreda = Convert.ToString(HttpContext.Request.Form["Woreda"]);
                driverss.LicenseNumber = Convert.ToString(HttpContext.Request.Form["LicenseNumber"]);
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        driverss.ProfilePicture = dataStream.ToArray();
                        driverss.LicensePicture = dataStream.ToArray();
                    }
                }
                _context.Update(driverss);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "You have updated your detail successfully";
                    return RedirectToAction(nameof(Create));    
                
            }
            else
            {


                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        driver.ProfilePicture = dataStream.ToArray();
                        driver.LicensePicture = dataStream.ToArray();
                    }
                }

                driver.StartDate = DateTime.Today;
                driver.Status = null;
                driver.UserId = users.Id;

               _context.Add(driver);
                await _context.SaveChangesAsync();

                TempData["Success"] = "You have registered successfully";
                return RedirectToAction(nameof(Create));
            }
            return View(driver);
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DriverId,FirstName,LastName,ExperienceInYears,Sex,Nationality,Date_of_birth,Mobile,Region,City,Subcity,Woreda,Status,StartDate,EndDate,LicenseNumber,ProfilePicture,License")] Driver driver)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.Id))
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
            return View(driver);
        }

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Driver.FindAsync(id);
            _context.Driver.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
      /*  private string UploadedFile(DriverViewModel model)
        {
            string uniqueFileName = null;
          
            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = System.IO.File.Create(filePath))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }

            
            }
            return uniqueFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string UploadedFiles(DriverViewModel model)
        {
          
            string uniqueFileNames = null;

            if (model.LicenseImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileNames = Guid.NewGuid().ToString() + "_" + model.LicenseImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileNames);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.LicenseImage.CopyTo(fileStream);
                }
            }


            return uniqueFileNames;
        }*/
        private bool DriverExists(int id)
        {
            return _context.Driver.Any(e => e.Id == id);
        }
    }
}
