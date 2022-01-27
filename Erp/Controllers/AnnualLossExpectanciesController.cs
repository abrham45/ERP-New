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
using System.IO;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;

namespace Erp.Controllers
{
    public class AnnualLossExpectanciesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public AnnualLossExpectanciesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AnnualLossExpectancies
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.AnnualLossExpectancy.Include(a => a.Risk);
            return View(await employeeContext.ToListAsync());
        }
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Risk-Team, Director")]
        public async Task<IActionResult> ExportToExcel()
        {
            var riskys = from a in _context.AnnualLossExpectancy.Include(e => e.AnnualLossExpectancys).Include(e => e.Risk) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AnnualLossExpectancy");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Risk";
                worksheet.Cell(currentRow, 3).Value = "ExposureFactor";
                worksheet.Cell(currentRow, 4).Value = "SingleLossExpectancy";
                worksheet.Cell(currentRow, 5).Value = "Value";
                worksheet.Cell(currentRow, 5).Value = "AnnualLossExpectancys";

                foreach (var i in riskys)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Risk.Name;
                        worksheet.Cell(currentRow, 3).Value = i.ExposureFactor;
                        worksheet.Cell(currentRow, 4).Value = i.SingleLossExpectancy;
                        worksheet.Cell(currentRow, 5).Value = i.Value;
                        worksheet.Cell(currentRow, 5).Value = i.AnnualLossExpectancys;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=AnnualLossExpectancy.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();


            }
        }
        // GET: AssetTypeRisks/Details/5
        // GET: AnnualLossExpectancies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annualLossExpectancy = await _context.AnnualLossExpectancy
                .Include(a => a.Risk)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annualLossExpectancy == null)
            {
                return NotFound();
            }

            return View(annualLossExpectancy);
        }

        // GET: AnnualLossExpectancies/Create
        public async Task<IActionResult> Create()
        {
            User users = await _userManager.GetUserAsync(User);
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return View();
            }
        }
    

        // POST: AnnualLossExpectancies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RiskId,ExposureFactor,SingleLossExpectancy,Value,AnnualLossExpectancys")] AnnualLossExpectancy annualLossExpectancy)
        {
            var AnnualOccurrences = Convert.ToDecimal(HttpContext.Request.Form["RiskId"]);
            var risk = _context.Risk.FirstOrDefault(e => e.AnnualOccurrence == AnnualOccurrences);

            if (risk != null)
            {
                annualLossExpectancy.RiskId = risk.Id;
                annualLossExpectancy.SingleLossExpectancy = annualLossExpectancy.ExposureFactor * annualLossExpectancy.Value;
                annualLossExpectancy.AnnualLossExpectancys = AnnualOccurrences * annualLossExpectancy.SingleLossExpectancy;
                _context.Add(annualLossExpectancy);
                await _context.SaveChangesAsync();
                return Redirect(nameof(Index));
            }
            else
            {

                TempData["Error"] = "The Annual Rate of Occurance enterd is not registed in risk";
                return RedirectToAction(nameof(Create));
            }


        }

        // GET: AnnualLossExpectancies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annualLossExpectancy = await _context.AnnualLossExpectancy.FindAsync(id);
            if (annualLossExpectancy == null)
            {
                return NotFound();
            }
            ViewData["RiskId"] = new SelectList(_context.Risk, "Id", "Id", annualLossExpectancy.RiskId);
            return View(annualLossExpectancy);
        }

        // POST: AnnualLossExpectancies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RiskId,ExposureFactor,SingleLossExpectancy,Value,AnnualLossExpectancys")] AnnualLossExpectancy annualLossExpectancy)
        {
            if (id != annualLossExpectancy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(annualLossExpectancy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnualLossExpectancyExists(annualLossExpectancy.Id))
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
            ViewData["RiskId"] = new SelectList(_context.Risk, "Id", "Id", annualLossExpectancy.RiskId);
            return View(annualLossExpectancy);
        }

        // GET: AnnualLossExpectancies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annualLossExpectancy = await _context.AnnualLossExpectancy
                .Include(a => a.Risk)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annualLossExpectancy == null)
            {
                return NotFound();
            }

            return View(annualLossExpectancy);
        }

        // POST: AnnualLossExpectancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var annualLossExpectancy = await _context.AnnualLossExpectancy.FindAsync(id);
            _context.AnnualLossExpectancy.Remove(annualLossExpectancy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnualLossExpectancyExists(int id)
        {
            return _context.AnnualLossExpectancy.Any(e => e.Id == id);
        }
    }
}
