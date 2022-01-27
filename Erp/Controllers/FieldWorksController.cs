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
    public class FieldWorksController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public FieldWorksController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FieldWorks
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (assetal != null)
            {
                var employeeContext = _context.FieldWork.Where(a=>a.EmployeeId==assetal.Id).Include(f => f.Employee).Include(f => f.Project).Include(f => f.Driver);
                return View(await employeeContext.ToListAsync());

            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> IndexUser()
        {

            var employeeContext = _context.FieldWork.Include(f => f.Employee).Include(f => f.Project).Include(f => f.Driver);
            return View(await employeeContext.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {

            var fildRequest = await _context.FieldWork.FindAsync(id);

            if (fildRequest == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    fildRequest.Status = true;
                    fildRequest.PerDay= Convert.ToInt32(HttpContext.Request.Form["PerDay"]);
                    _context.Update(fildRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldWorkExists(fildRequest.Id))
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

        }

        private bool LoanRequestsExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Reject(int id)
        {

            var fildRequest = await _context.FieldWork.FindAsync(id);

            if (fildRequest == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    fildRequest.Status = false;
                    _context.Update(fildRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldWorkExists(fildRequest.Id))
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

        }

        // GET: FieldWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var fieldWork = await _context.FieldWork
                .Include(f => f.Employee)
                .Include(f => f.Project)
                .Include(h=> h.Driver)
     
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fieldWork == null)
            {
                return NotFound();
            }


            ViewBag.ProjectId = new SelectList(_context.Project, "Id", "ProjectName");
            return View(fieldWork);
        }

        // GET: FieldWorks/Create
        public IActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_context.Project, "Id", "ProjectName");
            return View();
        }

        // POST: FieldWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Place,Duration,StartDate,ReturnDate,PerDay,EmployeeId,ProjectId,Status,DriverId")] FieldWork fieldWork)
        {
            {
                User users = await _userManager.GetUserAsync(User);
                var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
                fieldWork.Duration = Convert.ToInt32(fieldWork.ReturnDate.Day - fieldWork.StartDate.Day);
                if (emp != null)
                {
                    fieldWork.EmployeeId = emp.Id;
                    _context.Add(fieldWork);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


                ViewBag.ProjectId = new SelectList(_context.Project, "Id", "ProjectName", fieldWork.ProjectId);
                return View(fieldWork);
            }
        }
        // GET: FieldWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldWork = await _context.FieldWork.FindAsync(id);
            if (fieldWork == null)
            {
                return NotFound();
            }
            ViewBag.ProjectId = new SelectList(_context.Project, "Id", "ProjectName", fieldWork.ProjectId);
        
            return View(fieldWork);
        }


        // POST: FieldWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Place,Duration,StartDate,ReturnDate,PerDay,EmployeeId,ProjectId,Status")] FieldWork fieldWork)
        {
            if (id != fieldWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fieldWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldWorkExists(fieldWork.Id))
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
            ViewBag.ProjectId = new SelectList(_context.Project, "Id", "ProjectName", fieldWork.ProjectId);
            return View(fieldWork);
        }

        // GET: FieldWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldWork = await _context.FieldWork
                .Include(f => f.Employee)
                .Include(f => f.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fieldWork == null)
            {
                return NotFound();
            }

            return View(fieldWork);
        }

        // POST: FieldWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fieldWork = await _context.FieldWork.FindAsync(id);
            _context.FieldWork.Remove(fieldWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FieldWorkExists(int id)
        {
            return _context.FieldWork.Any(e => e.Id == id);
        }
    }
}
