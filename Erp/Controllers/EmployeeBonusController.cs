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
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Erp.Controllers
{
    [Authorize(Roles = "FinanceTeam")]
    public class EmployeeBonusController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeBonusController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: EmployeeBonus
        public async Task<IActionResult> Index(string empid)
        {
            var employeeContext = from a in _context.EmployeeBonus.Include(e => e.Bonus).Include(e => e.Employee)select a;

            if (!String.IsNullOrEmpty(empid))
            {
                employeeContext = employeeContext.Where(e => e.Employee.EmployeeCode.Contains(empid));
            }

            return View(await employeeContext.ToListAsync());
        }
        /// <returns></returns>
        [Authorize(Roles = "FinanceTeam")]
        public async Task<IActionResult> ExportToExcel()
        {
            var emp = from a in _context.EmployeeBonus.Include(e => e.Employee).Include(e=>e.Bonus) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("EmployeeBonus");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Bonus";
                worksheet.Cell(currentRow, 3).Value = "Employee";
                worksheet.Cell(currentRow, 4).Value = "EffectiveDate";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Bonus.Amount;
                        worksheet.Cell(currentRow, 3).Value = i.Employee.EmployeeCode;
                        worksheet.Cell(currentRow, 4).Value = i.EffectiveDate;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=EmployeeBonus.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
        /// <summary>.
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
            ViewData["BonusId"] = new SelectList(_context.BonusPolicy, "Id", "Name");
            return View();
        }

        // POST: EmployeeBonus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,BonusId,EffectiveDate")] EmployeeBonus employeeBonus)
        {
            var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var employee = _context.Employees.FirstOrDefault(q => q.EmployeeCode == emp);

            if (employee != null)
            {
                employeeBonus.EffectiveDate = DateTime.Today;
                employeeBonus.EmployeeId = employee.Id;

                _context.Add(employeeBonus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Warning"] = "Invalid Employee Id";
                return RedirectToAction(nameof(Create));
            }
            ViewData["BonusId"] = new SelectList(_context.BonusPolicy, "Id", "Name", employeeBonus.BonusId);
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
            ViewData["BonusId"] = new SelectList(_context.BonusPolicy, "Id", "Name", employeeBonus.BonusId);
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
            ViewData["BonusId"] = new SelectList(_context.BonusPolicy, "Id", "Name", employeeBonus.BonusId);
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
