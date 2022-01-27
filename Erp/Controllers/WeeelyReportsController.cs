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

namespace Erp.Controllers
{
    public class WeeelyReportsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public WeeelyReportsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: WeeelyReports
        public async Task<IActionResult> Index(int pageNumber = 1)
        {

            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
           
            if (emp != null)
            {
             
                var weeelyReport = _context.WeeelyReport.Where(e => e.EmployeeId == emp.Id).Include(e => e.Employees).OrderByDescending(s => s.Id);

                return View(await PaginatedList<WeeelyReport>.CreateAsync(weeelyReport, pageNumber, 10));
            }
            else
            {
                return NotFound();
            }

        }
    

        /*   return View(await _context.WeeelyReport.ToListAsync());*/
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> IndexSup(int pageNumber = 1)
        {
           

            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emp != null)
            {
                var weeelyReport = _context.WeeelyReport.Where(q => q.TeamId == emp.TeamId)
                                    .Where(e => e.EmployeeId != emp.Id)
                                    .Where(e => e.Employees.PositionId != 2)
                                    .Include(e => e.Employees)
                                    .OrderByDescending(s => s.Id);
                                  
                return View(await PaginatedList<WeeelyReport>.CreateAsync(weeelyReport, pageNumber, 10));
            }
            else
            {
                return NotFound();
            }

        }
        [Authorize(Roles = "TeamLeader")]
        public async Task<IActionResult> IndexT(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emp != null)
            {
                
            /*    var weeelyReport = _context.WeeelyReport.Where(q => q.Employees.DepartmentId == emp.DepartmentId).Where(e => e.EmployeeId != emp.Id).Include(e => e.Employees).Include(e => e.Team).Where(e=> e.status == null && e.Employees.PositionId == 3).ToList().OrderByDescending(s => s.TeamId);
*/
                var weeelyReport = _context.WeeelyReport
                    .Where(q => q.Employees.DepartmentId == emp.DepartmentId)
                    .Where(e => e.EmployeeId != emp.Id)
                    .Include(e => e.Employees)
                    .Include(e => e.Team).OrderByDescending(s => s.Id);

                return View(await PaginatedList<WeeelyReport>.CreateAsync(weeelyReport, pageNumber, 10));
            }
            else
            {
                return NotFound();
            }

        }
        [Authorize(Roles = "Director")]
        public async Task<IActionResult> IndexDir(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emp != null)
            {

                var weeelyReport = _context.WeeelyReport.OrderByDescending(s => s.Id)
                    .Where(d => d.Employees.PositionId == 2)
                 .Where(e => e.EmployeeId != emp.Id)
                           .Include(e => e.Employees);


                /*  var WeeelyReport = _context.WeeelyReport.Include(e => e.Employees).ToList().OrderByDescending(s => s.Id);*/

                return View(await PaginatedList<WeeelyReport>.CreateAsync(weeelyReport, pageNumber, 10));
            }
            else
            {
                return NotFound();
            }

        }


        // GET: WeeelyReports/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weeelyReport = await _context.WeeelyReport.Include(e=> e.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weeelyReport == null)
            {
                return NotFound();
            }

            return View(weeelyReport);
        }

        // GET: WeeelyReports/Create
        public async Task<IActionResult> Create()
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            return View();
        }

        // POST: WeeelyReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeeklyRecap,TaskRecap,TaskUnfinshed,From,To,Challenge,ChallengeOvercome,EmployeeId,TeamId")] WeeelyReport weeelyReport)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (emp!=null)
            {
                weeelyReport.To = DateTime.Today;
                weeelyReport.EmployeeId = emp.Id;
                weeelyReport.TeamId = emp.TeamId;
                _context.Add(weeelyReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weeelyReport);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approvedsup(int id)
        {

            var WeeelyReport = await _context.WeeelyReport.FindAsync(id);

            if (WeeelyReport == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    WeeelyReport.status = true;
                    _context.Update(WeeelyReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeeelyReportExists(WeeelyReport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
             
                
                return RedirectToAction(nameof(IndexSup));
            }

        }
        public async Task<IActionResult> Approvedtm(int id)
        {

            var WeeelyReport = await _context.WeeelyReport.FindAsync(id);

            if (WeeelyReport == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    WeeelyReport.status = true;
                    _context.Update(WeeelyReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeeelyReportExists(WeeelyReport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                return RedirectToAction(nameof(IndexT));
            }

        }
        public async Task<IActionResult> ApprovedDir(int id)
        {

            var WeeelyReport = await _context.WeeelyReport.FindAsync(id);

            if (WeeelyReport == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    WeeelyReport.status = true;
                    _context.Update(WeeelyReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeeelyReportExists(WeeelyReport.Id))
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
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public async Task<IActionResult> Reject(int id)
        {
            var WeeelyReport = await _context.WeeelyReport.FindAsync(id);

            if (WeeelyReport == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    WeeelyReport.status = false;
                    _context.Update(WeeelyReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeeelyReportExists(WeeelyReport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

    
                return RedirectToAction(nameof(IndexSup));
            }
        }
        // GET: WeeelyReports/Edit/5
        public async Task<IActionResult> Edit(int? id )
        { 
            if (id == null)
            {
                return NotFound();
            }

            var weekly = await _context.WeeelyReport.FindAsync(id);
            if (weekly == null)
            {
                return NotFound();
            }
            return View(weekly);

        }

        // POST: WeeelyReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeeklyRecap,TaskRecap,TaskUnfinshed,From,To,Challenge,ChallengeOvercome,EmployeeId,TeamId")] WeeelyReport weeelyReport)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

 
            if (id != weeelyReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    weeelyReport.To = DateTime.Today;
                    weeelyReport.EmployeeId = emp.Id;
                    weeelyReport.TeamId = emp.TeamId;
                    _context.Update(weeelyReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeeelyReportExists(weeelyReport.Id))
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
            return View(weeelyReport);
        }

        // GET: WeeelyReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weeelyReport = await _context.WeeelyReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weeelyReport == null)
            {
                return NotFound();
            }

            return View(weeelyReport);
        }

        // POST: WeeelyReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weeelyReport = await _context.WeeelyReport.FindAsync(id);
            _context.WeeelyReport.Remove(weeelyReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeeelyReportExists(int id)
        {
            return _context.WeeelyReport.Any(e => e.Id == id);
        }
    }
}
