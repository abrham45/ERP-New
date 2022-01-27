using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Erp.Controllers
{
    public class AssetTypeRisksController : Controller
    {
        private readonly EmployeeContext _context;

        public AssetTypeRisksController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: AssetTypeRisks
        public async Task<IActionResult> Index(string asstype)
        {
            var employeeContext = from m in _context.AssetTypeRisk.Include(a => a.Asset_types) select m;

            if (!String.IsNullOrEmpty(asstype))
            {
                employeeContext = employeeContext.Where(e => e.Asset_Type.Contains(asstype));
            }

            return View(await employeeContext.ToListAsync());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Risk-Team, Director")]
        public async Task<IActionResult> ExportToExcel()
        {
            var riskys = from a in _context.AssetTypeRisk.Include(e => e.Asset).Include(e=>e.Asset_Type) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AssetTypeRisk");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Asset Type";
                worksheet.Cell(currentRow, 3).Value = "Failure Probability";
                worksheet.Cell(currentRow, 4).Value = "Loss";
                worksheet.Cell(currentRow, 5).Value = "Total Asset Type Risk";

                foreach (var i in riskys)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Asset_types.Asset_Type;
                        worksheet.Cell(currentRow, 3).Value = i.FailureProbability;
                        worksheet.Cell(currentRow, 4).Value = i.Loss;
                        worksheet.Cell(currentRow, 5).Value = i.TotalAssetTypeRisk;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=AssetTypeRisk.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();


            }
        }
        // GET: AssetTypeRisks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetTypeRisk = await _context.AssetTypeRisk
                .Include(a => a.Asset_types)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetTypeRisk == null)
            {
                return NotFound();
            }

            return View(assetTypeRisk);
        }

        // GET: AssetTypeRisks/Create
        public IActionResult Create()
        {
            ViewBag.Asset_typeId = new SelectList(_context.Asset_type, "Id", "Asset_Type");
            return View();
        }

        // POST: AssetTypeRisks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Asset_typeId,FailureProbability,Loss,TotalAssetTypeRisk")] AssetTypeRisk assetTypeRisk)
        {
           
            if (ModelState.IsValid)
            {
                assetTypeRisk.TotalAssetTypeRisk = assetTypeRisk.FailureProbability * assetTypeRisk.Loss;
                _context.Add(assetTypeRisk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Asset_typeId = new SelectList(_context.Asset_type, "Id", "Asset_Type", assetTypeRisk.Asset_typeId);
            return View(assetTypeRisk);
        }

        // GET: AssetTypeRisks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetTypeRisk = await _context.AssetTypeRisk.FindAsync(id);
            if (assetTypeRisk == null)
            {
                return NotFound();
            }
            ViewData["Asset_typeId"] = new SelectList(_context.Asset_type, "Id", "Id", assetTypeRisk.Asset_typeId);
            return View(assetTypeRisk);
        }

        // POST: AssetTypeRisks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Asset_typeId,FailureProbability,Loss,TotalAssetTypeRisk")] AssetTypeRisk assetTypeRisk)
        {
            if (id != assetTypeRisk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetTypeRisk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetTypeRiskExists(assetTypeRisk.Id))
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
   
            return View(assetTypeRisk);
        }

        // GET: AssetTypeRisks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetTypeRisk = await _context.AssetTypeRisk
                .Include(a => a.Asset_types)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetTypeRisk == null)
            {
                return NotFound();
            }

            return View(assetTypeRisk);
        }

        // POST: AssetTypeRisks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetTypeRisk = await _context.AssetTypeRisk.FindAsync(id);
            _context.AssetTypeRisk.Remove(assetTypeRisk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetTypeRiskExists(int id)
        {
            return _context.AssetTypeRisk.Any(e => e.Id == id);
        }
    }
}
