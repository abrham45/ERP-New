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
    public class EmployeeAllowancesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeAllowancesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: EmployeeAllowances
        public async Task<IActionResult> Index(string empid)
        {
            var employeeContext = from a in  _context.EmployeeAllowance.Include(e => e.Allowance).Include(e => e.Employee) select a;

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
            var emp = from a in _context.EmployeeAllowance.Include(e => e.Employee).Include(e=>e.Allowance) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("EmployeeAllowance");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Allowance";
                worksheet.Cell(currentRow, 3).Value = "Employee";
                worksheet.Cell(currentRow, 4).Value = "EffectiveDate";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Allowance.Amount;
                        worksheet.Cell(currentRow, 3).Value = i.Employee.EmployeeCode;
                        worksheet.Cell(currentRow, 4).Value = i.EffectiveDate;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=EmployeeAllowance.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
        /// <summary>
        // GET: EmployeeAllowances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAllowance = await _context.EmployeeAllowance
                .Include(e => e.Allowance)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAllowance == null)
            {
                return NotFound();
            }

            return View(employeeAllowance);
        }

        // GET: EmployeeAllowances/Create
        public IActionResult Create()
        {
            ViewData["AllowanceId"] = new SelectList(_context.AllowancePolicy, "Id", "Name");
           
            return View();
        }

        // POST: EmployeeAllowances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,AllowanceId,EffectiveDate")] EmployeeAllowance employeeAllowance)
        {
            if (ModelState.IsValid)
            {
                var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
                var employee = _context.Employees.FirstOrDefault(q=>q.EmployeeCode == emp);

                if(employee != null)
                {
                    employeeAllowance.EffectiveDate = DateTime.Today;
                    employeeAllowance.EmployeeId = employee.Id;

                    _context.Add(employeeAllowance);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Warning"] = "Invalid Employee Id";
                    return RedirectToAction(nameof(Create));
                }
            }
            ViewData["AllowanceId"] = new SelectList(_context.AllowancePolicy, "Id", "Name", employeeAllowance.AllowanceId);
            
            return View(employeeAllowance);
        }

        // GET: EmployeeAllowances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAllowance = await _context.EmployeeAllowance.FindAsync(id);
            if (employeeAllowance == null)
            {
                return NotFound();
            }
            ViewData["AllowanceId"] = new SelectList(_context.AllowancePolicy, "Id", "Name", employeeAllowance.AllowanceId);
       
            return View(employeeAllowance);
        }

        // POST: EmployeeAllowances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,AllowanceId,EffectiveDate")] EmployeeAllowance employeeAllowance)
        {
            if (id != employeeAllowance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
                    var employee = _context.Employees.FirstOrDefault(q => q.EmployeeCode == emp);

                    if (employee != null)
                    {
                        var thisemp = _context.EmployeeAllowance.FirstOrDefault(e => e.EmployeeId == employee.Id);

                        if (thisemp != null)
                        {
                            employeeAllowance.EmployeeId = employee.Id;
                            employeeAllowance.EffectiveDate = DateTime.Today;

                            _context.Update(employeeAllowance);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Edit));
                        }
                        else
                        {
                            TempData["Warning"] = "Invalid Employee Id";
                            return RedirectToAction(nameof(Edit));
                        }
                    }
                    else
                    {
                        TempData["Warning"] = "Invalid Employee Id";
                        return RedirectToAction(nameof(Edit));
                    }
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeAllowanceExists(employeeAllowance.Id))
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
            ViewData["AllowanceId"] = new SelectList(_context.AllowancePolicy, "Id", "Name", employeeAllowance.AllowanceId);
            return View(employeeAllowance);
        }

        // GET: EmployeeAllowances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAllowance = await _context.EmployeeAllowance
                .Include(e => e.Allowance)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAllowance == null)
            {
                return NotFound();
            }

            return View(employeeAllowance);
        }

        // POST: EmployeeAllowances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeAllowance = await _context.EmployeeAllowance.FindAsync(id);
            _context.EmployeeAllowance.Remove(employeeAllowance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAllowanceExists(int id)
        {
            return _context.EmployeeAllowance.Any(e => e.Id == id);
        }
    }
}
