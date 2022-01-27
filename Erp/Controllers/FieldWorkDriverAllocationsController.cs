using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;

namespace Erp.Controllers
{
    public class FieldWorkDriverAllocationsController : Controller
    {
        private readonly EmployeeContext _context;

        public FieldWorkDriverAllocationsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: FieldWorkDriverAllocations
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.FieldWorkDriverAllocation.Include(f => f.Driver).Include(f => f.FieldWork);
            return View(await employeeContext.ToListAsync());
        }

        // GET: FieldWorkDriverAllocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldWorkDriverAllocation = await _context.FieldWorkDriverAllocation
                .Include(f => f.Driver)
                .Include(f => f.FieldWork)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fieldWorkDriverAllocation == null)
            {
                return NotFound();
            }

            return View(fieldWorkDriverAllocation);
        }

        // GET: FieldWorkDriverAllocations/Create
        public IActionResult Create()
        {
            ViewBag.FieldWorkId = new SelectList(_context.FieldWork, "Id", "Place");
            ViewBag.FieldWorkIds = new SelectList(_context.FieldWork, "Id", "Duration");
            ViewBag.FieldWorkIdss = new SelectList(_context.FieldWork, "Id", "StartDate");

            return View();
        }

        // POST: FieldWorkDriverAllocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FieldWorkId,DriverId,PerDay,EmployeeId,StartDay")] FieldWorkDriverAllocation fieldWorkDriverAllocation)
        {
            var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var empd = _context.Employees.FirstOrDefault(e => e.EmployeeCode == emp);

            if (empd!=null)
            {
                fieldWorkDriverAllocation.EmployeeId = empd.Id;

                _context.Add(fieldWorkDriverAllocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "No Employee With This ID";
                return RedirectToAction(nameof(Create));
            }
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "City", fieldWorkDriverAllocation.DriverId);
            ViewData["FieldWorkId"] = new SelectList(_context.FieldWork, "Id", "Id", fieldWorkDriverAllocation.FieldWorkId);
            return View(fieldWorkDriverAllocation);
        }

        // GET: FieldWorkDriverAllocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldWorkDriverAllocation = await _context.FieldWorkDriverAllocation.FindAsync(id);
            if (fieldWorkDriverAllocation == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "City", fieldWorkDriverAllocation.DriverId);
            ViewData["FieldWorkId"] = new SelectList(_context.FieldWork, "Id", "Id", fieldWorkDriverAllocation.FieldWorkId);
            return View(fieldWorkDriverAllocation);
        }

        // POST: FieldWorkDriverAllocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FieldWorkId,DriverId,PerDay")] FieldWorkDriverAllocation fieldWorkDriverAllocation)
        {
            if (id != fieldWorkDriverAllocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fieldWorkDriverAllocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldWorkDriverAllocationExists(fieldWorkDriverAllocation.Id))
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
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "City", fieldWorkDriverAllocation.DriverId);
            ViewData["FieldWorkId"] = new SelectList(_context.FieldWork, "Id", "Id", fieldWorkDriverAllocation.FieldWorkId);
            return View(fieldWorkDriverAllocation);
        }

        // GET: FieldWorkDriverAllocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldWorkDriverAllocation = await _context.FieldWorkDriverAllocation
                .Include(f => f.Driver)
                .Include(f => f.FieldWork)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fieldWorkDriverAllocation == null)
            {
                return NotFound();
            }

            return View(fieldWorkDriverAllocation);
        }

        // POST: FieldWorkDriverAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fieldWorkDriverAllocation = await _context.FieldWorkDriverAllocation.FindAsync(id);
            _context.FieldWorkDriverAllocation.Remove(fieldWorkDriverAllocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FieldWorkDriverAllocationExists(int id)
        {
            return _context.FieldWorkDriverAllocation.Any(e => e.Id == id);
        }
    }
}
