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
    public class ComplaintsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public ComplaintsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Complaints
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
             
            if(emp != null)
            {
                var complaint = _context.Complaint.Where(q => q.EmployeeId == emp.Id).OrderByDescending(s => s.Id);
                
                return View(await PaginatedList<Complaint>.CreateAsync(complaint, pageNumber, 7));
            }
            else
            {
                return NotFound();
            }

        }
        public async Task<IActionResult> IndexDriver()
        {
            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Driver.FirstOrDefault(a => a.UserId == user.Id);
            if (assetal != null)
            {
                var roc = _context.Complaint.Where(e => e.DriverId == assetal.Id);
                return View(await roc.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// supervisorrrrrrrrrrrrrrrrrrrrrrrrr
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles= ("Supervisor"))]
        public async Task<IActionResult> IndexSup(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emp != null)
            {
                var complaint = _context.Complaint.Include(a => a.Employee).Where(q => q.TeamId == emp.TeamId).Where(q=> q.EmployeeId != emp.Id).OrderByDescending(s => s.Id);

                return View(await PaginatedList<Complaint>.CreateAsync(complaint, pageNumber, 7));
            }
            else
            {
                return NotFound();
            }

        }
        [Authorize(Roles= ("TeamLeader"))]
        public async Task<IActionResult> IndexTeam(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emp != null)
            {
                var complaint = _context.Complaint.Include(a=>a.Employee).Where(q => q.TeamId == emp.TeamId).Where(q => q.EmployeeId != emp.Id).OrderByDescending(s => s.Id);

                return View(await PaginatedList<Complaint>.CreateAsync(complaint, pageNumber, 7));
            }
            else
            {
                return NotFound();
            }

        }
        [Authorize(Roles = ("Director"))]
        public async Task<IActionResult> IndexDir(int pageNumber = 1)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emp != null)
            {
                var complaint = _context.Complaint.Include(a => a.Employee).Where(q => q.status == true).Where(q => q.EmployeeId != emp.Id).OrderByDescending(s => s.Id);

                return View(await PaginatedList<Complaint>.CreateAsync(complaint, pageNumber, 7));
            }
            else
            {
                return NotFound();
            }

        }
        // POST: Asset_Exchange/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approved(int id)
        {

            var complaint = await _context.Complaint.FindAsync(id);

            if (complaint == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    complaint.status = true;
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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
        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public async Task<IActionResult> Reject(int id)
        {
            var complaint = await _context.Complaint.FindAsync(id);

            if (complaint == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    complaint.status = false;
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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
        // GET: Asset_Exchange/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,TeamId,DepartmentId,Subject,Description,SentDate,DriverId")] Complaint complaint)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            var dive = _context.Driver.FirstOrDefault(e => e.UserId == user.Id);

            if (ModelState.IsValid)
            {
                if(emp != null)
                {
                    complaint.EmployeeId = emp.Id;
                    complaint.TeamId = emp.TeamId;
                    complaint.SentDate = DateTime.Today;
                    complaint.status = null;
                    _context.Add(complaint);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else if (dive != null & emp == null)

                {
                    complaint.DriverId = dive.Id;
                    complaint.status = null;

                    complaint.SentDate = DateTime.Today;


                    _context.Add(complaint);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexDriver));

                }
                else
                {
                    TempData["Error"] = "Fill your Employee Detail before creating complaint";
                    return RedirectToAction(nameof(Create));
                }
                
            }
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }  
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,,DepartmentId,TeamId,Subject,Description,SentDate")] Complaint complaint)
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (id != complaint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    complaint.EmployeeId = emp.Id;
                    complaint.TeamId = emp.TeamId;

                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaint.FindAsync(id);
            _context.Complaint.Remove(complaint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaint.Any(e => e.Id == id);
        }
    }
}
