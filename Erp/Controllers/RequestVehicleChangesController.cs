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
    public class RequestVehicleChangesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public RequestVehicleChangesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: RequestVehicleChanges
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (assetal != null)
            {
                var empall = _context.RequestVehicleChange.Where(e => e.EmployeeId == assetal.Id);
                return View(await PaginatedList<RequestVehicleChange>.CreateAsync(empall.Include(j=>j.Vechicles), pageNumber, 7));

            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> IndexFleet(int pageNumber = 1)
        {

            return View(await PaginatedList<RequestVehicleChange>.CreateAsync(_context.RequestVehicleChange.Include(a=>a.Vechicles), pageNumber, 7));


        }


        // GET: RequestVehicleChanges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestVehicleChange = await _context.RequestVehicleChange
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestVehicleChange == null)
            {
                return NotFound();
            }

            return View(requestVehicleChange);
        }

        // GET: RequestVehicleChanges/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: RequestVehicleChanges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleId,EmployeeId,Status,Feedback,Date")] RequestVehicleChange requestVehicleChange)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
            var vec = Convert.ToString(HttpContext.Request.Form["VehicleId"]);
            var vecl = _context.vechicles.FirstOrDefault(q => q.PlateNumber == vec);

            if (vecl!= null & emp!=null)
            {

                requestVehicleChange.EmployeeId = emp.Id;
                requestVehicleChange.Status = null;

                requestVehicleChange.Date = DateTime.Today;
                requestVehicleChange.VehicleId = vecl.Id;


                _context.Add(requestVehicleChange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else
            {

                TempData["Error"] = "No Vehicle Found with This Plate Number";
                return RedirectToAction(nameof(Create));
            }

        }

        // GET: RequestVehicleChanges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestVehicleChange = await _context.RequestVehicleChange.FindAsync(id);
            if (requestVehicleChange == null)
            {
                return NotFound();
            }
         
            return View(requestVehicleChange);
        }

        public async Task<IActionResult> Approved(int id)
        {

            var vicle_req = await _context.RequestVehicleChange.FindAsync(id);

            if (vicle_req == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    vicle_req.Feedback = Convert.ToString(HttpContext.Request.Form["Feedback"]);
                    vicle_req.Status = true;
                    _context.Update(vicle_req);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestVehicleChangeExists(vicle_req.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexFleet));
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reject(int id)
        {
            var vicle_req = await _context.RequestVehicleChange.FindAsync(id);

            if (vicle_req == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    vicle_req.Status = false;
                    _context.Update(vicle_req);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestVehicleChangeExists(vicle_req.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexFleet));
            }
        }





        // POST: RequestVehicleChanges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleId,EmployeeId,Status,Feedback,Date")] RequestVehicleChange requestVehicleChange)
        {
            if (id != requestVehicleChange.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestVehicleChange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestVehicleChangeExists(requestVehicleChange.Id))
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
     
            return View(requestVehicleChange);
        }

        // GET: RequestVehicleChanges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestVehicleChange = await _context.RequestVehicleChange
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestVehicleChange == null)
            {
                return NotFound();
            }

            return View(requestVehicleChange);
        }

        // POST: RequestVehicleChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requestVehicleChange = await _context.RequestVehicleChange.FindAsync(id);
            _context.RequestVehicleChange.Remove(requestVehicleChange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestVehicleChangeExists(int id)
        {
            return _context.RequestVehicleChange.Any(e => e.Id == id);
        }
    }
}
