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
    public class BudgetsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public BudgetsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Budgets
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Budget.Include(b => b.DirectorPlan).Include(a=>a.Employee);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Budgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget
                .Include(b => b.DirectorPlan)
               
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: Budgets/Create
        public IActionResult Create()
        {
            ViewBag.DirectorPlanId = new SelectList(_context.DirectorPlan, "Id", "Name");
       
            return View();
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DirectorPlanId,Description,OverheadCosts,TimeCosts,ExternalCost,TotalCost,Status,EmployeeId")] Budget budget)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (emp != null)
            {
                budget.TotalCost = budget.TimeCosts + budget.OverheadCosts + budget.ExternalCost;

                budget.EmployeeId = emp.Id;
                budget.Status = null;

                ViewBag.DirectorPlanId = new SelectList(_context.DirectorPlan, "Id", "Name", budget.DirectorPlanId);
                _context.Add(budget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
       
           
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            ViewData["DirectorPlanId"] = new SelectList(_context.DirectorPlan, "Id", "Name", budget.DirectorPlanId);
            
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DirectorPlanId,Description,OverheadCosts,TimeCosts,ExternalCost,TotalCost,Status,EmployeeId")] Budget budget)
        {
            if (id != budget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.Id))
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
            ViewData["DirectorPlanId"] = new SelectList(_context.DirectorPlan, "Id", "Name", budget.DirectorPlanId);
    
            return View(budget);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approved(int id)
        {

            var asset_req = await _context.Budget.FindAsync(id);

            if (asset_req == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    asset_req.Status = true;
                    _context.Update(asset_req);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(asset_req.Id))
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
            var asset_req = await _context.Budget.FindAsync(id);

            if (asset_req == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    asset_req.Status = false;
                    _context.Update(asset_req);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(asset_req.Id))
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
        // GET: Budgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget
                .Include(b => b.DirectorPlan)
            
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budget = await _context.Budget.FindAsync(id);
            _context.Budget.Remove(budget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return _context.Budget.Any(e => e.Id == id);
        }
    }
}
