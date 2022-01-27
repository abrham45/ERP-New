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

namespace Erp.Controllers
{
    public class DMaintainancesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public DMaintainancesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DMaintainances
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.DMaintainance.Include(d => d.Driver).Include(d => d.vechicles);
            return View(await employeeContext.ToListAsync());
        }
        // GET: DMaintainances
        public async Task<IActionResult> IndexUser()
        {
            User users = await _userManager.GetUserAsync(User);
            var driverss = _context.Driver.FirstOrDefault(e => e.UserId == users.Id);
            var employeeContext = _context.DMaintainance
                .Include(d => d.Driver)
                .Include(d => d.vechicles).Where(e => e.DriverId == driverss.Id);

            return View( employeeContext.ToList());
        }

        // GET: DMaintainances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dMaintainance = await _context.DMaintainance
                .Include(d => d.Driver)
                .Include(d => d.vechicles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dMaintainance == null)
            {
                return NotFound();
            }

            return View(dMaintainance);
        }

        // GET: DMaintainances/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: DMaintainances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,vechiclesId,Status,Feedback,Date,Description,DriverId")] DMaintainance dMaintainance)
        {
         /*   var description = Convert.ToString(HttpContext.Request.Form[""])*/
            User users = await _userManager.GetUserAsync(User);
            var driverss = _context.Driver.FirstOrDefault(q => q.UserId == users.Id);

           
            if (driverss != null)
            {
                var vechicleAll = _context.DriverAllocation.FirstOrDefault(q => q.DriverId == driverss.Id);

                if (vechicleAll != null )
                {
                   
                    dMaintainance.Date = DateTime.Today;
                    dMaintainance.Status = null;
                    dMaintainance.vechiclesId = vechicleAll.VehicleId;
                    dMaintainance.DriverId = driverss.Id;

                    _context.Add(dMaintainance);
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

            return View(dMaintainance);
        }

        // GET: DMaintainances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dMaintainance = await _context.DMaintainance.FindAsync(id);
            if (dMaintainance == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "City", dMaintainance.DriverId);
            ViewData["vechiclesId"] = new SelectList(_context.vechicles, "Id", "Id", dMaintainance.vechiclesId);
            return View(dMaintainance);
        }

        // POST: DMaintainances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,vechiclesId,Status,Feedback,Date,Description,DriverId")] DMaintainance dMaintainance)
        {
            if (id != dMaintainance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dMaintainance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DMaintainanceExists(dMaintainance.Id))
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
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "City", dMaintainance.DriverId);
            ViewData["vechiclesId"] = new SelectList(_context.vechicles, "Id", "Id", dMaintainance.vechiclesId);
            return View(dMaintainance);
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

            var maintain = await _context.DMaintainance.FindAsync(id);
            var feed = Convert.ToString(HttpContext.Request.Form["name"]);

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
                    if (!DMaintainanceExists(maintain.Id))
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

            var maintain = await _context.DMaintainance.FindAsync(id);
            var feed = Convert.ToString(HttpContext.Request.Form["name"]);

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
                    if (!DMaintainanceExists(maintain.Id))
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


        // GET: DMaintainances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dMaintainance = await _context.DMaintainance
                .Include(d => d.Driver)
                .Include(d => d.vechicles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dMaintainance == null)
            {
                return NotFound();
            }

            return View(dMaintainance);
        }

        // POST: DMaintainances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dMaintainance = await _context.DMaintainance.FindAsync(id);
            _context.DMaintainance.Remove(dMaintainance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DMaintainanceExists(int id)
        {
            return _context.DMaintainance.Any(e => e.Id == id);
        }
    }
}
