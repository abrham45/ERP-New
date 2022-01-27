﻿using System;
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
    public class EmployeeDeductionsController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeDeductionsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: EmployeeDeductions
        public async Task<IActionResult> Index(string empid)
        {
            var employeeContext = from a in _context.EmployeeDeduction.Include(e => e.DeductionPolicy).Include(e => e.Employee) select a;

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
            var emp = from a in _context.EmployeeDeduction.Include(e => e.Employee).Include(e=>e.DeductionPolicy) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("EmployeeDeduction");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Deduction";
                worksheet.Cell(currentRow, 3).Value = "Employee";
                worksheet.Cell(currentRow, 4).Value = "EffectiveDate";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.DeductionPolicy.Amount;
                        worksheet.Cell(currentRow, 3).Value = i.Employee.EmployeeCode;
                        worksheet.Cell(currentRow, 4).Value = i.EffectiveDate;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=EmployeeDeduction.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
        /// <summary>.
        // GET: EmployeeDeductions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDeduction = await _context.EmployeeDeduction
                .Include(e => e.DeductionPolicy)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeDeduction == null)
            {
                return NotFound();
            }

            return View(employeeDeduction);
        }

        // GET: EmployeeDeductions/Create
        public IActionResult Create()
        {
            ViewData["DeductionPolicyId"] = new SelectList(_context.DeductionPolicy, "Id", "Name");
            return View();
        }

        // POST: EmployeeDeductions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,DeductionPolicyId,EffectiveDate")] EmployeeDeduction employeeDeduction)
        {
        
            var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
            var employee = _context.Employees.FirstOrDefault(q => q.EmployeeCode == emp);

            if (employee != null)
            {
                employeeDeduction.EmployeeId = employee.Id;
                employeeDeduction.EffectiveDate = DateTime.Today;

                _context.Add(employeeDeduction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Warning"] = "Invalid Employee Id";
                return RedirectToAction(nameof(Create));
            }
            ViewData["DeductionPolicyId"] = new SelectList(_context.DeductionPolicy, "Id", "Name", employeeDeduction.DeductionPolicyId);
            
            return View(employeeDeduction);
        }

        // GET: EmployeeDeductions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDeduction = await _context.EmployeeDeduction.FindAsync(id);
            if (employeeDeduction == null)
            {
                return NotFound();
            }
            ViewData["DeductionPolicyId"] = new SelectList(_context.DeductionPolicy, "Id", "Name", employeeDeduction.DeductionPolicyId);
          
            return View(employeeDeduction);
        }

        // POST: EmployeeDeductions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,DeductionPolicyId,EffectiveDate")] EmployeeDeduction employeeDeduction)
        {
            if (id != employeeDeduction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var emp = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
                    var employee = _context.Employees.FirstOrDefault(q => q.EmployeeCode == emp);
                    if(employee != null)
                    {
                        var thisemp = _context.EmployeeDeduction.FirstOrDefault(e => e.EmployeeId == employee.Id);
                       
                        if ( thisemp != null)
                        {
                            employeeDeduction.EmployeeId = employee.Id;
                            employeeDeduction.EffectiveDate = DateTime.Today;

                            _context.Update(employeeDeduction);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
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
                    if (!EmployeeDeductionExists(employeeDeduction.Id))
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
            ViewData["DeductionPolicyId"] = new SelectList(_context.DeductionPolicy, "Id", "Name", employeeDeduction.DeductionPolicyId);
           
            return View(employeeDeduction);
        }

        // GET: EmployeeDeductions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDeduction = await _context.EmployeeDeduction
                .Include(e => e.DeductionPolicy)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeDeduction == null)
            {
                return NotFound();
            }

            return View(employeeDeduction);
        }

        // POST: EmployeeDeductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeDeduction = await _context.EmployeeDeduction.FindAsync(id);
            _context.EmployeeDeduction.Remove(employeeDeduction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDeductionExists(int id)
        {
            return _context.EmployeeDeduction.Any(e => e.Id == id);
        }
    }
}
