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
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Erp.Controllers
{
    [Authorize(Roles = "FinanceTeam,Director")]
    public class SalariesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public SalariesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Salaries
        public async Task<IActionResult> Index(string empid)
        {
            var gross = from m in _context.Salary.Include(e => e.Employee) select m ;

            if (!String.IsNullOrEmpty(empid))
            {
                gross = gross.Where(e => e.Employee.EmployeeCode == empid);
            }

            return View(await gross.ToListAsync());
        }
        /// <returns></returns>
        [Authorize(Roles = "FinanceTeam")]
        public async Task<IActionResult> ExportToExcel()
        {
            var emp = from a in _context.Salary.Include(e => e.Employee) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("GrossSalary");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "GrossSalary";
                worksheet.Cell(currentRow, 3).Value = "Employee";
                worksheet.Cell(currentRow, 4).Value = "TaxedSalary";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.GrossSalary;
                        worksheet.Cell(currentRow, 3).Value = i.Employee.EmployeeCode;
                        worksheet.Cell(currentRow, 4).Value = i.TaxedSalary;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=GrossSalary.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
        /// <summary>
        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
               /* .Include(e => e.Tax)*/
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // GET: Salaries/Create
        public IActionResult Create()
        {
           
            return View();
        }


        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GrossSalary,EmployeeId,TaxedSalary")] Salary salary)
        {
          
            var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var grossSalary = Convert.ToDecimal(HttpContext.Request.Form["GrossSalary"]);
            Decimal tax = 0;

            if (emp != null)
            {
                var emps = _context.Employees.FirstOrDefault(q => q.EmployeeCode == emp);
                var employee = _context.Employees.FirstOrDefault(q => q.EmployeeCode == emp);
                var ko = _context.Salary.AsEnumerable().LastOrDefault(e => e.EmployeeId == emps.Id);

                if (employee != null & ko == null)
                {

                    if (grossSalary <= 600)
                    {
                        tax = 0;
                        salary.TaxedSalary = grossSalary;
                    }
                    else if (601 <= grossSalary & grossSalary <= 1650)
                    {

                        tax = (grossSalary - Convert.ToDecimal(600.00)) * Convert.ToDecimal(0.10);
                        salary.TaxedSalary = grossSalary - tax;
                    }
                    else if (1651 <= grossSalary & grossSalary <= 3250)
                    {

                        tax = Convert.ToDecimal(165.00) + (grossSalary - Convert.ToDecimal(1650)) * Convert.ToDecimal(0.15);
                        salary.TaxedSalary = grossSalary - tax;
                    }
                    else if (3251 <= grossSalary & grossSalary <= 5250)
                    {

                        tax = Convert.ToDecimal(405.00) + (grossSalary - Convert.ToDecimal(3250.00)) * Convert.ToDecimal(0.20);
                        salary.TaxedSalary = grossSalary - tax;
                    }
                    else if (5251 <= grossSalary & grossSalary <= 7800)
                    {

                        tax = Convert.ToDecimal(805.00) + (grossSalary - Convert.ToDecimal(5250.00)) * Convert.ToDecimal(0.25);
                        salary.TaxedSalary = grossSalary - tax;
                    }
                    else if (7801 <= grossSalary & grossSalary <= 10900)
                    {

                        tax = Convert.ToDecimal(1442.50) + (grossSalary - Convert.ToDecimal(7800.00)) * Convert.ToDecimal(0.30);
                        salary.TaxedSalary = grossSalary - tax;
                    }
                    else if (grossSalary >= 10900)
                    {

                        tax = Convert.ToDecimal(2372.50) + (grossSalary - Convert.ToDecimal(10900.00)) * Convert.ToDecimal(0.35);
                        salary.TaxedSalary = grossSalary - tax;
                    }

                    salary.EmployeeId = employee.Id;

                    _context.Add(salary);
                    await _context.SaveChangesAsync();


                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Warning"] = "Invalid Employee ID or Growth Salary Already  Assigned";

                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                TempData["Warning"] = "Invalid Employee Id";

                return RedirectToAction(nameof(Create));
            }

        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }

         
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GrossSalary,EmployeeId,TaxId")] Salary salary)
        {
            if (id != salary.Id)
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
                        salary.EmployeeId = employee.Id;
                        _context.Add(salary);

                        _context.Update(salary);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TempData["Warning"] = "Invalid Employee ID...";
                        return RedirectToAction(nameof(Create));
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.Id))
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

          
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salary = await _context.Salary.FindAsync(id);
            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return _context.Salary.Any(e => e.Id == id);
        }
    }
}
