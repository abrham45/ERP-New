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
    public class IssueSaleriesController : Controller
    {
        private readonly EmployeeContext _context;

        public IssueSaleriesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: IssueSaleries
        [Authorize(Roles = "FinanceTeam, Director")]
        public async Task<IActionResult> ExportToExcel()
        {
            var riskys = from a in _context.IssueSalery
                         .Include(e => e.Employee)
                         .Include(e => e.Salary)
                         select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("NetSalery");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "EmployeeId";
                worksheet.Cell(currentRow, 3).Value = "Total Alllowance";
                worksheet.Cell(currentRow, 4).Value = "Total Deduction";
                worksheet.Cell(currentRow, 5).Value = "Gross Salary";
                worksheet.Cell(currentRow, 6).Value = "Taxed Salary";
                worksheet.Cell(currentRow, 7).Value = "Net Amount";


                foreach (var i in riskys)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Employee.EmployeeCode;
                        worksheet.Cell(currentRow, 3).Value = i.TotalAlllowance;
                        worksheet.Cell(currentRow, 4).Value = i.TotalDeduction;
                        worksheet.Cell(currentRow, 5).Value = i.Salary.GrossSalary;
                        worksheet.Cell(currentRow, 6).Value = i.Salary.TaxedSalary;
                        worksheet.Cell(currentRow, 7).Value = i.NetAmount;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=NetSalery.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();


            }
        }
        // GET: AssetRisks/Details/5
        // GET: IssueSaleries
        public async Task<IActionResult> Index(string empid)
        {
            var employeeContext = from m in _context.IssueSalery.Include(i => i.Employee).Include(i => i.EmployeeAllowance).Include(i => i.EmployeeBonus).Include(i => i.LoanRequest) select m;

            if (!String.IsNullOrEmpty(empid))
            {
                employeeContext = employeeContext.Where(e => e.Employee.EmployeeCode == empid);
            }
            return View(await employeeContext.ToListAsync());
        }
        // GET: IssueSaleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueSalery = await _context.IssueSalery
                .Include(i => i.Employee)
                .Include(i => i.EmployeeAllowance)
                .Include(i => i.EmployeeBonus)
                .Include(i => i.EmployeeBonus.Bonus)
                .Include(i => i.EmployeeAllowance.Allowance)
                .Include(i => i.LoanRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueSalery == null)
            {
                return NotFound();
            }

            return View(issueSalery);
        }

        // GET: IssueSaleries/Create/*
    
        public IActionResult Create()
        {
          
            return View();
        }

        // POST: IssueSaleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IssueDate,Status,TotalSalary,NetAmount,TotalDeduction,TotalAlllowance,EmployeeId,LoanRequestId,EmployeeBonusId,EmployeeAllowanceId")] IssueSalery issueSalery)
        {
            var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var employee = _context.Employees.FirstOrDefault(q => q.EmployeeCode == emp);

            if (employee != null)
            {

                var allowance = _context.EmployeeAllowance.Include(e => e.Allowance).FirstOrDefault(q => q.EmployeeId == employee.Id);
                var bonus = _context.EmployeeBonus.Include(e => e.Bonus).FirstOrDefault(q => q.EmployeeId == employee.Id);
                var loan = _context.LoanRequest.FirstOrDefault(q => q.EmployeeId == employee.Id);
                var deduction = _context.EmployeeDeduction.Include(e => e.DeductionPolicy).FirstOrDefault(q => q.EmployeeId == employee.Id);        
                var grossSalary = _context.Salary.FirstOrDefault(q => q.EmployeeId == employee.Id);
                var empsal = _context.IssueSalery.AsEnumerable().LastOrDefault(e => e.EmployeeId == employee.Id);

                var mondeduction= _context.EmployeeDeduction.AsEnumerable().LastOrDefault(e => e.EmployeeId == employee.Id); // for  deduction is this month.
                var monallowance = _context.EmployeeAllowance.AsEnumerable().LastOrDefault(e => e.EmployeeId == employee.Id); // for  deduction is this month.

                var bmc = _context.EmployeeBonus.AsEnumerable().LastOrDefault(w => w.EmployeeId == employee.Id);
          /*    var empsal = _context.IssueSalery.AsEnumerable().Where(e => e.EmployeeId == employee.Id);*/     

                if (empsal != null)
                {
                    if (empsal.IssueDate.Month != DateTime.Today.Month)
                    {



                        if (deduction != null & allowance != null & grossSalary != null)
                        {

                            var totalDeduction = deduction.DeductionPolicy.Amount;

                            if (loan != null)
                            {
                                totalDeduction = loan.EachMonthDeductionAmount + deduction.DeductionPolicy.Amount;
                            }
                            var boun = bonus.Bonus.Amount;


                            var totalAllowance = allowance.Allowance.Amount; //bonus not here  
                           
                            var NetAmount = grossSalary.TaxedSalary + (totalAllowance );
                            issueSalery.EmployeeId = employee.Id;
                            issueSalery.IssueDate = DateTime.Today;
                            issueSalery.Status = null;
                            issueSalery.SalaryId = grossSalary.Id;
                            issueSalery.EmployeeAllowanceId = allowance.Id;
                            issueSalery.EmployeeBonusId = bonus.Id; //Bonus Id not here
                          /*  issueSalery.TotalDeduction = totalDeduction;*/
                            issueSalery.TotalAlllowance = totalAllowance;
                            issueSalery.LoanRequestId = null; //loan payment Id not here

                            if (loan != null)
                            {
                                issueSalery.LoanRequestId = loan.Id; //loan payment Id not here
                                loan.LeftLoanAmount = loan.LeftLoanAmount - loan.EachMonthDeductionAmount;
                            }

                            issueSalery.NetAmount = NetAmount;
                            _context.Add(issueSalery);
                            await _context.SaveChangesAsync();

                            var model = await _context.IssueSalery.FirstOrDefaultAsync(q => q.EmployeeId == employee.Id);
                            if (model != null)
                            {
                                return View(model);
                            }

                            else
                            {
                                TempData["Warning"] = "SorryTry Again later...";
                                return RedirectToAction(nameof(Create));
                            }

                        }
                        else if (deduction != null & allowance == null & grossSalary != null)
                        {
                           /* var totalDeduction = deduction.DeductionPolicy.Amount;*/
                            var NetAmount = grossSalary.TaxedSalary ;
                            issueSalery.EmployeeId = employee.Id;
                            issueSalery.IssueDate = DateTime.Today;
                            issueSalery.Status = null;
                            issueSalery.SalaryId = grossSalary.Id;
                            issueSalery.NetAmount = NetAmount;

                            _context.Add(issueSalery);
                            await _context.SaveChangesAsync();
                        }

                        else if (deduction == null & allowance != null & grossSalary != null)
                        {
                            var totalAllowance = allowance.Allowance.Amount; //bonus not here  
                            var NetAmount = grossSalary.TaxedSalary + totalAllowance;
                            issueSalery.EmployeeId = employee.Id;
                            issueSalery.IssueDate = DateTime.Today;
                            issueSalery.Status = null;
                            issueSalery.SalaryId = grossSalary.Id;
                            issueSalery.NetAmount = NetAmount;

                            _context.Add(issueSalery);
                            await _context.SaveChangesAsync();
                        }
                        else if (deduction == null & allowance == null & grossSalary == null)
                        {
                            var NetAmount = grossSalary.TaxedSalary;

                            issueSalery.EmployeeId = employee.Id;
                            issueSalery.IssueDate = DateTime.Today;
                            issueSalery.Status = null;
                            issueSalery.SalaryId = grossSalary.Id;


                            _context.Add(issueSalery);
                            await _context.SaveChangesAsync();

                        }
                        else
                        {
                            var NetAmount = grossSalary.TaxedSalary;
                            issueSalery.EmployeeId = employee.Id;
                            issueSalery.IssueDate = DateTime.Today;
                            issueSalery.Status = null;
                            issueSalery.SalaryId = grossSalary.Id;
                            issueSalery.NetAmount = NetAmount;

                            _context.Add(issueSalery);
                            await _context.SaveChangesAsync();
                        }

                }
                    else

                    {
                        TempData["Warning"] = "salary is calulated this month";
                        return RedirectToAction(nameof(Create));
                    }

                   
                }

                else

                {
                    if (deduction != null & allowance != null & grossSalary != null)
                    {
                       /* v*//*ar totalDeduction = deduction.DeductionPolicy.Amount;*/

                        if (loan != null)
                        {
                       /*     totalDeduction = loan.EachMonthDeductionAmount + deduction.DeductionPolicy.Amount;*/
                        }
                        /*    var boun = bonus.Bonus.Amount;*/
                        var totalAllowance = allowance.Allowance.Amount; //bonus not here  

                        var NetAmount = grossSalary.TaxedSalary + (totalAllowance);
                        issueSalery.EmployeeId = employee.Id;
                        issueSalery.IssueDate = DateTime.Today;
                        issueSalery.Status = null;
                        issueSalery.SalaryId = grossSalary.Id;
                        issueSalery.EmployeeAllowanceId = allowance.Id;
                        issueSalery.EmployeeBonusId = bonus.Id; //Bonus Id not here
               /*         issueSalery.TotalDeduction = totalDeduction;*/
                        issueSalery.TotalAlllowance = totalAllowance;
                        issueSalery.LoanRequestId = null; //loan payment Id not here

                        if (loan != null)
                        {
                            issueSalery.LoanRequestId = loan.Id; //loan payment Id not here
                            loan.LeftLoanAmount = loan.LeftLoanAmount - loan.EachMonthDeductionAmount;
                        }

                        issueSalery.NetAmount = NetAmount;
                        _context.Add(issueSalery);
                        await _context.SaveChangesAsync();

                        var model = await _context.IssueSalery.FirstOrDefaultAsync(q => q.EmployeeId == employee.Id);
                        if (model != null)
                        {
                            return View(model);
                        }

                        else
                        {
                            TempData["Warning"] = "SorryTry Again later...";
                            return RedirectToAction(nameof(Create));
                        }

                    }
                    else if (deduction != null & allowance == null & grossSalary != null)
                    {
        /*                var totalDeduction = deduction.DeductionPolicy.Amount;*/
                        var NetAmount = grossSalary.TaxedSalary ;
                        issueSalery.EmployeeId = employee.Id;
                        issueSalery.IssueDate = DateTime.Today;
                        issueSalery.Status = null;
                        issueSalery.SalaryId = grossSalary.Id;
                        issueSalery.NetAmount = NetAmount;

                        _context.Add(issueSalery);
                        await _context.SaveChangesAsync();
                    }

                    else if (deduction == null & allowance != null & grossSalary != null)
                    {
                        var totalAllowance = allowance.Allowance.Amount; //bonus not here  
                        var NetAmount = grossSalary.TaxedSalary + totalAllowance;
                        issueSalery.EmployeeId = employee.Id;
                        issueSalery.IssueDate = DateTime.Today;
                        issueSalery.Status = null;
                        issueSalery.SalaryId = grossSalary.Id;
                        issueSalery.NetAmount = NetAmount;

                        _context.Add(issueSalery);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        var NetAmount = grossSalary.TaxedSalary;
                        issueSalery.EmployeeId = employee.Id;
                        issueSalery.IssueDate = DateTime.Today;
                        issueSalery.Status = null;
                        issueSalery.SalaryId = grossSalary.Id;
                        issueSalery.NetAmount = NetAmount;

                        _context.Add(issueSalery);
                        await _context.SaveChangesAsync();
                    }

                }


            }

            else
            {
                TempData["Warning"] = "Invalid Employee Id";
                return RedirectToAction(nameof(Create));
            }
           
            ViewData["EmployeeAllowanceId"] = new SelectList(_context.EmployeeAllowance, "Id", "Name", issueSalery.EmployeeAllowanceId);
            ViewData["EmployeeBonusId"] = new SelectList(_context.Set<EmployeeBonus>(), "Id", "Name", issueSalery.EmployeeBonusId);
            ViewData["LoanRequestId"] = new SelectList(_context.Set<LoanPayment>(), "Id", "Name", issueSalery.LoanRequestId);
            return View(issueSalery);
        }

        // GET: IssueSaleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueSalery = await _context.IssueSalery.FindAsync(id);
            if (issueSalery == null)
            {
                return NotFound();
            }
            ViewData["EmployeeAllowanceId"] = new SelectList(_context.EmployeeAllowance, "Id", "Name", issueSalery.EmployeeAllowanceId);
            ViewData["EmployeeBonusId"] = new SelectList(_context.Set<EmployeeBonus>(), "Id", "Name", issueSalery.EmployeeBonusId);
            ViewData["LoanRequestId"] = new SelectList(_context.Set<LoanPayment>(), "Id", "Name", issueSalery.LoanRequestId);
            return View(issueSalery);
        }

        // POST: IssueSaleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IssueDate,Status,TotalSalary,NetAmount,TotalDeduction,TotalAlllowance,EmployeeId,LoanRequestId,EmployeeBonusId,EmployeeAllowanceId")] IssueSalery issueSalery)
        {
            if (id != issueSalery.Id)
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
                        issueSalery.EmployeeId = employee.Id;
                        issueSalery.IssueDate = DateTime.Today;

                        _context.Update(issueSalery);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        TempData["Warning"] = "Invalid Employee Id";
                        return RedirectToAction(nameof(Create));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueSaleryExists(issueSalery.Id))
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
            ViewData["EmployeeAllowanceId"] = new SelectList(_context.EmployeeAllowance, "Id", "Name", issueSalery.EmployeeAllowanceId);
            ViewData["EmployeeBonusId"] = new SelectList(_context.Set<EmployeeBonus>(), "Id", "Name", issueSalery.EmployeeBonusId);
            ViewData["LoanRequestId"] = new SelectList(_context.Set<LoanPayment>(), "Id", "Name", issueSalery.LoanRequestId);
            return View(issueSalery);
        }

        // GET: IssueSaleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueSalery = await _context.IssueSalery
                .Include(i => i.Employee)
                .Include(i => i.EmployeeAllowance)
                .Include(i => i.EmployeeBonus)
                .Include(i => i.LoanRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueSalery == null)
            {
                return NotFound();
            }

            return View(issueSalery);
        }

        // POST: IssueSaleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueSalery = await _context.IssueSalery.FindAsync(id);
            _context.IssueSalery.Remove(issueSalery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueSaleryExists(int id)
        {
            return _context.IssueSalery.Any(e => e.Id == id);
        }
    }
}
