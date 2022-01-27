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
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Erp.Controllers
{
    public class LoanRequestsController : Controller
    {
        private readonly EmployeeContext _context;

        private readonly UserManager<User> _userManager;
        public LoanRequestsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: LoanRequests
        public async Task<IActionResult> Index()
        {
            var users = _userManager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(q => q.UserId == users);

            if (emp != null)
            {
                var employeeContext = _context.LoanRequest.Include(l => l.LoanPolicy).Where(l => l.EmployeeId == emp.Id);
                return View(await employeeContext.ToListAsync());
            }
            else
            {
                TempData["Warning"] = "Please fill your Employment Details.";
                return RedirectToAction(nameof(Index));
            }

           
        }
        /// <returns></returns>
        [Authorize(Roles = "Finance")]
        public async Task<IActionResult> ExportToExcel()
        {
            var riskys = from a in _context.LoanRequest.Include(e=>e.Employee).Include(e => e.LoanPolicy) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LoanRequest");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Status";
                worksheet.Cell(currentRow, 3).Value = "Description";
                worksheet.Cell(currentRow, 4).Value = "StartDate";
                worksheet.Cell(currentRow, 5).Value = "EndDate";
                worksheet.Cell(currentRow, 6).Value = "EachMonthDeductionAmount";
                worksheet.Cell(currentRow, 7).Value = "Date";
                worksheet.Cell(currentRow, 8).Value = "TotalLoanAmount";
                worksheet.Cell(currentRow, 9).Value = "LeftLoanAmount";
                worksheet.Cell(currentRow, 10).Value = "TotalLoanAmount";
                worksheet.Cell(currentRow, 11).Value = "LoanPolicy";
                worksheet.Cell(currentRow, 12).Value = "Employee";

                foreach (var i in riskys)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Status;
                        worksheet.Cell(currentRow, 3).Value = i.Description;
                        worksheet.Cell(currentRow, 4).Value = i.StartDate;
                        worksheet.Cell(currentRow, 5).Value = i.EndDate;
                        worksheet.Cell(currentRow, 6).Value = i.EachMonthDeductionAmount;
                        worksheet.Cell(currentRow, 7).Value = i.Date;
                        worksheet.Cell(currentRow, 8).Value = i.TotalLoanAmount;
                        worksheet.Cell(currentRow, 9).Value = i.LeftLoanAmount;
                        worksheet.Cell(currentRow, 10).Value = i.TotalLoanAmount;
                        worksheet.Cell(currentRow, 11).Value = i.LoanPolicy.Name;
                        worksheet.Cell(currentRow, 12).Value = i.Employee.EmployeeCode;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=LoanRequest.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();


            }
        }
        // GET: AssetTypeRisks/Details/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {

            var LoanRequest = await _context.LoanRequest.FindAsync(id);

            if (LoanRequest == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    LoanRequest.Status = true;
                    _context.Update(LoanRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanRequestsExists(LoanRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexDir));
            }

        }

        private bool LoanRequestsExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Reject(int id)
        {

            var LoanRequest = await _context.LoanRequest.FindAsync(id);

            if (LoanRequest == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    LoanRequest.Status = false;
                    _context.Update(LoanRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanRequestsExists(LoanRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexDir));
            }

        }
        // GET: DepartmentChanges/Delete/5
        /// <summary>
        /// 
        // GET: LoanRequests
        public async Task<IActionResult> IndexFinance()
        {
            var employeeContext = _context.LoanRequest.Include(l => l.Employee).Include(l => l.LoanPolicy);
            return View(await employeeContext.ToListAsync());
        }

        // GET: LoanRequests
        public async Task<IActionResult> IndexDir()
        {
            var users = _userManager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(q => q.UserId == users);

            if (emp != null)
            {
                var employee = _context.Employees.Where(e=>e.DivisionId == emp.DivisionId).Select(e => e.Id).ToList();
                var loans = _context.LoanRequest.Include(l => l.Employee).Include(l => l.LoanPolicy).Where(e => employee.Contains(e.Id));

                return View(await loans.ToListAsync());
            }
            else
            {
                TempData["Warning"] = "Please fill your Employment Details.";
                return RedirectToAction(nameof(Index));
            }
        }


        // GET: LoanRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRequest = await _context.LoanRequest
                .Include(l => l.Employee)
                .Include(l => l.LoanPolicy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanRequest == null)
            {
                return NotFound();
            }

            return View(loanRequest);
        }

        // GET: LoanRequests/Create
        public IActionResult Create()
        {
            ViewBag.LoanPolicyId = new SelectList(_context.LoanPolicy, "Id", "Name");
            return View();
        }

        // POST: LoanRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,Description,StartDate,EndDate,EachMonthDeductionAmount,Date,TotalLoanAmount,LeftLoanAmount,LoanPolicyId,EmployeeId")] LoanRequest loanRequest)
        {
            var users = _userManager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(q => q.UserId == users);

            if (ModelState.IsValid)
            {
                loanRequest.Date = DateTime.Today;
                loanRequest.Status = null;
                loanRequest.EmployeeId = emp.Id;
             /*   loanRequest.LeftLoanAmount = loanRequest.TotalLoanAmount;*/

                _context.Add(loanRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.LoanPolicyId = new SelectList(_context.LoanPolicy, "Id", "Name", loanRequest.LoanPolicyId);
            return View(loanRequest);
        }

        // GET: LoanRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRequest = await _context.LoanRequest.FindAsync(id);
            if (loanRequest == null)
            {
                return NotFound();
            }
            ViewBag.LoanPolicyId = new SelectList(_context.LoanPolicy, "Id", "Name", loanRequest.LoanPolicyId);
            return View(loanRequest);
        }

        // POST: LoanRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Description,StartDate,EndDate,EachMonthDeductionAmount,Date,TotalLoanAmount,LeftLoanAmount,LoanPolicyId,EmployeeId")] LoanRequest loanRequest)
        {
            if (id != loanRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var users = _userManager.GetUserId(User);
                    var emp = _context.Employees.FirstOrDefault(q => q.UserId == users);

               
                        loanRequest.Date = DateTime.Today;
                        loanRequest.Status = null;
                        loanRequest.EmployeeId = emp.Id;

                        _context.Update(loanRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanRequestExists(loanRequest.Id))
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
            ViewBag.LoanPolicyId = new SelectList(_context.LoanPolicy, "Id", "Name", loanRequest.LoanPolicyId);
            return View(loanRequest);
        }

        // GET: LoanRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRequest = await _context.LoanRequest
                .Include(l => l.Employee)
                .Include(l => l.LoanPolicy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanRequest == null)
            {
                return NotFound();
            }

            return View(loanRequest);
        }

        // POST: LoanRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanRequest = await _context.LoanRequest.FindAsync(id);
            _context.LoanRequest.Remove(loanRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanRequestExists(int id)
        {
            return _context.LoanRequest.Any(e => e.Id == id);
        }
    }
}
