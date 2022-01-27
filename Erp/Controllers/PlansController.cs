using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;

namespace Erp.Controllers
{
    public class PlansController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public PlansController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Plans
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Plan.Include(p => p.Employee);
            return View(await employeeContext.ToListAsync());
        }
        public async Task<IActionResult> IndexUser()
        {
            var cont = _userManager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == cont);

            var employeeContext = _context.Plan.Include(p => p.Employee).Where(e=>e.EmployeeId == emp.Id);
            return View(await employeeContext.ToListAsync());
        }
        // GET: Plans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .Include(p => p.Employee)
                 .Include(p => p.PlanType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // GET: Plans/Create
        public IActionResult Create()
        {
            ViewBag.PlanTypeId = new SelectList(_context.PlanType, "Id", "Name");
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FiscalYear,Period,PlanTypeId,Description,ProjectNumber,Status,OperationExpense,EffectiveDate,EmployeeId")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                var cont = _userManager.GetUserId(User);
                var emp = _context.Employees.FirstOrDefault(e => e.UserId == cont);

                plan.EmployeeId = emp.Id;
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PlanTypeId = new SelectList(_context.PlanType, "Id", "Name", plan.PlanTypeId);
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            ViewBag.PlanTypeId = new SelectList(_context.PlanType, "Id", "Name", plan.PlanTypeId);
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FiscalYear,Period,PlanTypeId,Description,ProjectNumber,Status,OperationExpense,EffectiveDate,EmployeeId")] Plan plan)
        {
            if (id != plan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.Id))
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
            ViewBag.PlanTypeId = new SelectList(_context.PlanType, "Id", "Name", plan.PlanTypeId);
            return View(plan);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<IActionResult> Approve(int id)
        {

            var planss = await _context.Plan.FindAsync(id);

            if (planss == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    planss.Status = true;
                    _context.Update(planss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(planss.Id))
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reject(int id)
        {
            var planss = await _context.Plan.FindAsync(id);

            if (planss == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    planss.Status = false;
                    _context.Update(planss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(planss.Id))
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
        // GET: Plans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Plan.FindAsync(id);
            _context.Plan.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanExists(int id)
        {
            return _context.Plan.Any(e => e.Id == id);
        }
    }
}
