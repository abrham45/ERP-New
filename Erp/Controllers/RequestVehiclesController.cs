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
    public class RequestVehiclesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public RequestVehiclesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: RequestVehicles
        public async Task<IActionResult> Index(int pageNumber = 1)
        {

            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (assetal != null)
            {
                var empall = _context.RequestVehicle.Where(e => e.EmployeeId == assetal.Id);
                return View(await PaginatedList<RequestVehicle>.CreateAsync(empall, pageNumber, 7));

            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> IndexDir()
        {

            var users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.Where(e => e.DepartmentId == users.DepartmentId).Select(x => x.Id).ToList();


            var assetRequests = _context.RequestVehicle.Include(e => e.Employees).Where(e => emp.Contains(e.EmployeeId)).ToList();


            return View(assetRequests);

        }
        public async Task<IActionResult> IndexFeelt(int pageNumber = 1)
        {

            return View(await PaginatedList<RequestVehicle>.CreateAsync(_context.RequestVehicle.Include(e=>e.Employees), pageNumber, 7));


        }


        // GET: RequestVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestVehicle = await _context.RequestVehicle
                .Include(r => r.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestVehicle == null)
            {
                return NotFound();
            }

            return View(requestVehicle);
        }

        // GET: RequestVehicles/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: RequestVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Status,Feedback,Date,EmployeeId,RouteId,Destination")] RequestVehicle requestVehicle)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (emp != null)
            {

                requestVehicle.EmployeeId = emp.Id;
                requestVehicle.Status = null;
           
                requestVehicle.Date = DateTime.Today;


                _context.Add(requestVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

           
            return View(requestVehicle);

       
        }

        // GET: RequestVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestVehicle = await _context.RequestVehicle.FindAsync(id);
            if (requestVehicle == null)
            {
                return NotFound();
            }
      
            return View(requestVehicle);
        }

        public async Task<IActionResult> Approved(int id)
        {

            var vicle_req = await _context.RequestVehicle.FindAsync(id);

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
                    if (!RequestVehicleExists(vicle_req.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexFeelt));
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reject(int id)
        {
            var vicle_req = await _context.RequestVehicle.FindAsync(id);

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
                    if (!RequestVehicleExists(vicle_req.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexFeelt));
            }
        }




        // POST: RequestVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Status,Feedback,Date,EmployeeId")] RequestVehicle requestVehicle)
        {
            if (id != requestVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestVehicleExists(requestVehicle.Id))
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
            ViewBag.RouteId = new SelectList(_context.Route, "Id", "Destination", requestVehicle.RouteId);
            return View(requestVehicle);
        }

        // GET: RequestVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestVehicle = await _context.RequestVehicle
                .Include(r => r.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestVehicle == null)
            {
                return NotFound();
            }

            return View(requestVehicle);
        }

        // POST: RequestVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requestVehicle = await _context.RequestVehicle.FindAsync(id);
            _context.RequestVehicle.Remove(requestVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestVehicleExists(int id)
        {
            return _context.RequestVehicle.Any(e => e.Id == id);
        }
    }
}
