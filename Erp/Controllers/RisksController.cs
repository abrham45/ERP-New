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
    public class RisksController : Controller
    {
        private readonly EmployeeContext _context;

        public RisksController(EmployeeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Risk-Team, Director")]
        public async Task<IActionResult> ExportToExcel()
        {
            var riskys = from a in _context.Risk.Include(e=>e.RiskType) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Risks");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Priority";
                worksheet.Cell(currentRow, 4).Value = "AnnualOccurrence";
                worksheet.Cell(currentRow, 5).Value = "Likelyhood";
                worksheet.Cell(currentRow, 6).Value = "Created_at";
                worksheet.Cell(currentRow, 7).Value = "Imapact";
                worksheet.Cell(currentRow, 8).Value = "Status";
                worksheet.Cell(currentRow, 9).Value = "RiskType";
               

                foreach (var i in riskys)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Name;
                        worksheet.Cell(currentRow, 3).Value = i.Priority;
                        worksheet.Cell(currentRow, 4).Value = i.AnnualOccurrence;
                        worksheet.Cell(currentRow, 5).Value = i.Likelyhood;
                        worksheet.Cell(currentRow, 6).Value = i.Created_at;
                        worksheet.Cell(currentRow, 7).Value = i.Imapact;
                        worksheet.Cell(currentRow, 8).Value = i.Status;
                        worksheet.Cell(currentRow, 9).Value = i.RiskType.Name;
                      

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=Risks.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();


            }
        }
        // GET: Employees/Details/5
        // GET: Risks
        [Authorize(Roles = "Risk-Team, Director")]
        public async Task<IActionResult> Index(string risky, int pageNumber = 1)
        {
            var employeeContext = from m in  _context.Risk.Include(r => r.RiskType)select m;

            if (!String.IsNullOrEmpty(risky))
            {
                employeeContext = employeeContext.Where(e => e.Name.Contains(risky));
            }

            return View(await PaginatedList<Risk>.CreateAsync(employeeContext, pageNumber, 10));
        }

        // GET: Risks/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var risk = await _context.Risk
                .Include(r => r.RiskType)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (risk == null)
            {
                return NotFound();
            }

            return View(risk);
        }

        // GET: Risks/Create
        public IActionResult Create()
        {
            ViewBag.RiskTypeId = new SelectList(_context.RiskType, "Id", "Name");
            return View();
        }

        // POST: Risks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Priority,AnnualOccurrence,Likelyhood,Created_at,Imapact,Status,RiskTypeId")] Risk risk)
        {
          

            if (ModelState.IsValid)
            {
                risk.Created_at = DateTime.Now;
                risk.Status = Convert.ToString(HttpContext.Request.Form["Status"]);
                _context.Add(risk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RiskTypeId = new SelectList(_context.RiskType, "Id", "Name",risk.RiskTypeId);
            return View(risk);
        }

        // GET: Risks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var risk = await _context.Risk.FindAsync(id);
            if (risk == null)
            {
                return NotFound();
            }
            ViewBag.RiskTypeId = new SelectList(_context.RiskType, "Id", "Name");
            return View(risk);
        }

        // POST: Risks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Priority,AnnualOccurrence,Likelyhood,Created_at,Imapact,Status,RiskTypeId")] Risk risk)
        {
            if (id != risk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(risk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiskExists(risk.Id))
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
            ViewBag.RiskTypeId = new SelectList(_context.RiskType, "Id", "Name", risk.RiskTypeId);
            return View(risk);
        }

        // GET: Risks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var risk = await _context.Risk
                .Include(r => r.RiskType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (risk == null)
            {
                return NotFound();
            }

            return View(risk);
        }

        // POST: Risks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var risk = await _context.Risk.FindAsync(id);
            _context.Risk.Remove(risk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiskExists(int id)
        {
            return _context.Risk.Any(e => e.Id == id);
        }
    }
}
