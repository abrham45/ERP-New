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
    public class AssetRisksController : Controller
    {
        private readonly EmployeeContext _context;

        public AssetRisksController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: AssetRisks
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var employeeContext = _context.AssetRisk.Include(a => a.Assets);
     
            return View(await PaginatedList<AssetRisk>.CreateAsync(employeeContext, pageNumber, 7));
        }

        [Authorize(Roles = "Risk-Team, Director")]
        public async Task<IActionResult> ExportToExcel()
        {
            var riskys = from a in _context.AssetRisk.Include(e=>e.Assets) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AssetRisks");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "AssetName";
                worksheet.Cell(currentRow, 3).Value = "serial_number";
                worksheet.Cell(currentRow, 4).Value = "AnnualOccurrence";
                worksheet.Cell(currentRow, 5).Value = "factory_number";
                worksheet.Cell(currentRow, 6).Value = "Loss";
                worksheet.Cell(currentRow, 7).Value = "FailureProbability";
                worksheet.Cell(currentRow, 8).Value = "AssetRisks";


                foreach (var i in riskys)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Assets.Asset_Name;
                        worksheet.Cell(currentRow, 2).Value = i.Assets.serial_number;
                        worksheet.Cell(currentRow, 2).Value = i.Assets.factory_number;
                        worksheet.Cell(currentRow, 3).Value = i.Loss;
                        worksheet.Cell(currentRow, 4).Value = i.FailureProbability;
                        worksheet.Cell(currentRow, 5).Value = i.AssetRisks;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=AssetRisks.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();


            }
        }
        // GET: AssetRisks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetRisk = await _context.AssetRisk
                .Include(a => a.Assets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetRisk == null)
            {
                return NotFound();
            }

            return View(assetRisk);
        }

        // GET: AssetRisks/Create
        public IActionResult Create()
        {
            ViewBag.RiskId = new SelectList(_context.Risk, "Id", "Name");
            return View();
        }

        // POST: AssetRisks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssetId,Loss,FailureProbability,AssetRisks")] AssetRisk assetRisk)
        {
            var factNum = Convert.ToString(HttpContext.Request.Form["AssetId"]);
            var asset = _context.Asset.FirstOrDefault(e => e.factory_number == factNum);
            if (asset != null)
            {
               
                assetRisk.AssetRisks = assetRisk.FailureProbability * assetRisk.Loss;
                _context.Add(assetRisk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                TempData["Error"] = " Invalid Asset or Asset Factory Number";
                return RedirectToAction(nameof(Create));
            }
            ViewBag.RiskId = new SelectList(_context.Risk, "Id", "Name", assetRisk.RiskId);
            return View(assetRisk);
        }

        // GET: AssetRisks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetRisk = await _context.AssetRisk.FindAsync(id);
            if (assetRisk == null)
            {
                return NotFound();
            }
            ViewBag.RiskId = new SelectList(_context.Risk, "Id", "Name");
            return View(assetRisk);
        }

        // POST: AssetRisks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssetId,Loss,FailureProbability,AssetRisks")] AssetRisk assetRisk)
        {
            if (id != assetRisk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetRisk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetRiskExists(assetRisk.Id))
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
            ViewBag.RiskId = new SelectList(_context.Risk, "Id", "Name", assetRisk.RiskId);
            return View(assetRisk);
        }

        // GET: AssetRisks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetRisk = await _context.AssetRisk
                .Include(a => a.Assets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetRisk == null)
            {
                return NotFound();
            }

            return View(assetRisk);
        }

        // POST: AssetRisks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetRisk = await _context.AssetRisk.FindAsync(id);
            _context.AssetRisk.Remove(assetRisk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetRiskExists(int id)
        {
            return _context.AssetRisk.Any(e => e.Id == id);
        }
    }
}
