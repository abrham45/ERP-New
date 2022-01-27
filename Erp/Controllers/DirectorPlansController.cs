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
    public class DirectorPlansController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public DirectorPlansController(EmployeeContext context ,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DirectorPlans
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (emp != null)
            {
                var employeeContext = _context.DirectorPlan.Where(a => a.EmployeeId == emp.Id).Include(d => d.Employee);
                return View(await employeeContext.ToListAsync());
            }

            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> IndexMin()
        {

            var employeeContext = _context.DirectorPlan.Include(d => d.Employee);
            return View(await employeeContext.ToListAsync());
        }



        // GET: DirectorPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorPlan = await _context.DirectorPlan
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorPlan == null)
            {
                return NotFound();
            }

            return View(directorPlan);
        }

        // GET: DirectorPlans/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: DirectorPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FiscalYear,Period,Description,ProjectNumber,Status,OperationExpense,EffectiveDate,EmployeeId")] DirectorPlan directorPlan)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
            if (ModelState.IsValid)
            {
                directorPlan.EmployeeId = emp.Id;

                directorPlan.Status = null;
               
                _context.Add(directorPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(directorPlan);
        }

        // GET: DirectorPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorPlan = await _context.DirectorPlan.FindAsync(id);
            if (directorPlan == null)
            {
                return NotFound();
            }
           
            return View(directorPlan);
        }

        // POST: DirectorPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FiscalYear,Period,Description,ProjectNumber,Status,OperationExpense,EffectiveDate,EmployeeId")] DirectorPlan directorPlan)
        {
            if (id != directorPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directorPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorPlanExists(directorPlan.Id))
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
       
            return View(directorPlan);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approved(int id)
        {

            var dp = await _context.DirectorPlan.FindAsync(id);

            if (dp == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    dp.Status = true;
                    _context.Update(dp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorPlanExists(dp.Id))
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
        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public async Task<IActionResult> Reject(int id)
        {
            var dp = await _context.DirectorPlan.FindAsync(id);

            if (dp == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    dp.Status = false;
                    _context.Update(dp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorPlanExists(dp.Id))
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
        // GET: DirectorPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorPlan = await _context.DirectorPlan
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorPlan == null)
            {
                return NotFound();
            }

            return View(directorPlan);
        }

        // POST: DirectorPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var directorPlan = await _context.DirectorPlan.FindAsync(id);
            _context.DirectorPlan.Remove(directorPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorPlanExists(int id)
        {
            return _context.DirectorPlan.Any(e => e.Id == id);
        }
    }
}
