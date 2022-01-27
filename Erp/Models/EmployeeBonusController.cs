using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;

namespace Erp.Models
{
    public class EmployeeBonusController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeBonusController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: EmployeeBonus
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.EmployeeBonus.Include(e => e.Bonus).Include(e => e.Employee);
            return View(await employeeContext.ToListAsync());
        }

        // GET: EmployeeBonus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeBonus = await _context.EmployeeBonus
                .Include(e => e.Bonus)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeBonus == null)
            {
                return NotFound();
            }

            return View(employeeBonus);
        }

        // GET: EmployeeBonus/Create
        public IActionResult Create()
        {
            ViewData["BonusId"] = new SelectList(_context.Set<BonusPolicy>(), "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City");
            return View();
        }

        // POST: EmployeeBonus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,BonusId,EffectiveDate")] EmployeeBonus employeeBonus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeBonus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BonusId"] = new SelectList(_context.Set<BonusPolicy>(), "Id", "Id", employeeBonus.BonusId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City", employeeBonus.EmployeeId);
            return View(employeeBonus);
        }

        // GET: EmployeeBonus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeBonus = await _context.EmployeeBonus.FindAsync(id);
            if (employeeBonus == null)
            {
                return NotFound();
            }
            ViewData["BonusId"] = new SelectList(_context.Set<BonusPolicy>(), "Id", "Id", employeeBonus.BonusId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City", employeeBonus.EmployeeId);
            return View(employeeBonus);
        }

        // POST: EmployeeBonus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,BonusId,EffectiveDate")] EmployeeBonus employeeBonus)
        {
            if (id != employeeBonus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeBonus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeBonusExists(employeeBonus.Id))
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
            ViewData["BonusId"] = new SelectList(_context.Set<BonusPolicy>(), "Id", "Id", employeeBonus.BonusId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "City", employeeBonus.EmployeeId);
            return View(employeeBonus);
        }

        // GET: EmployeeBonus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeBonus = await _context.EmployeeBonus
                .Include(e => e.Bonus)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeBonus == null)
            {
                return NotFound();
            }

            return View(employeeBonus);
        }

        // POST: EmployeeBonus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeBonus = await _context.EmployeeBonus.FindAsync(id);
            _context.EmployeeBonus.Remove(employeeBonus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeBonusExists(int id)
        {
            return _context.EmployeeBonus.Any(e => e.Id == id);
        }
    }
}
