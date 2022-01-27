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
    public class vechiclesController : Controller
    {
        private readonly EmployeeContext _context;

        public vechiclesController(EmployeeContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "FleetTeam,Director")]
        // GET: vechicles
        public async Task<IActionResult> Index(string vehicl)
        {
            var vehicles = from m in _context.vechicles select m;

            if (!String.IsNullOrEmpty(vehicl))
            {
                vehicles = vehicles.Where(s => s.PlateNumber.Contains(vehicl));
            }

            return View(await vehicles.ToListAsync());
        }
        /*   public async Task<IActionResult> Index(string vehicle, int pageNumber = 1)
           {


               var employees = from m in _context.Employees.OrderBy(e => e.Team).Include(a => a.Team) select m;

               if (!String.IsNullOrEmpty(EmployeeCode))
               {
                   employees = employees.Where(s => s.EmployeeCode.Contains(EmployeeCode));
               }
               return View(await PaginatedList<Employee>.CreateAsync(employees, pageNumber, 10));

           }*/
/*
        public async Task<IActionResult> ExportToExcel()
        {
            var emp = from a in _context.vechicles.Include(e => e.vechiclesType) select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Vehicles");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Model";
                worksheet.Cell(currentRow, 3).Value = "vechiclesType";
                worksheet.Cell(currentRow, 4).Value = "PlateNumber";
                worksheet.Cell(currentRow, 5).Value = "Insurance";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.Model;
                        worksheet.Cell(currentRow, 3).Value = i.vechiclesType.Type;
                        worksheet.Cell(currentRow, 4).Value = i.PlateNumber;
                        worksheet.Cell(currentRow, 5).Value = i.IsInsured;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=vehicles.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }
            }
        }*/
        [Authorize(Roles = "FleetTeam,Director")]
        // GET: vechicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vechicles = await _context.vechicles
                .Include(v => v.vechiclesType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vechicles == null)
            {
                return NotFound();
            }

            return View(vechicles);
        }

        [Authorize(Roles = "FleetTeam")]
        // GET: vechicles/Create
        public IActionResult Create()
        {
            ViewData["vechiclesTypeId"] = new SelectList(_context.vechiclesType, "id", "Type");
            return View();
        }

        // POST: vechicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "FleetTeam")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,vechiclesTypeId,PlateNumber,IsInsured")] vechicles vechicles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vechicles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["vechiclesTypeId"] = new SelectList(_context.vechiclesType, "id", "Type", vechicles.vechiclesTypeId);
            return View(vechicles);
        }


        [Authorize(Roles = "FleetTeam")]
        // GET: vechicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vechicles = await _context.vechicles.FindAsync(id);
            if (vechicles == null)
            {
                return NotFound();
            }
            ViewData["vechiclesTypeId"] = new SelectList(_context.vechiclesType, "id", "Type", vechicles.vechiclesTypeId);
            return View(vechicles);
        }

        [Authorize(Roles = "FleetTeam")]
        // POST: vechicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,vechiclesTypeId,PlateNumber,IsInsured")] vechicles vechicles)
        {
            if (id != vechicles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vechicles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!vechiclesExists(vechicles.Id))
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
            ViewData["vechiclesTypeId"] = new SelectList(_context.vechiclesType, "id", "Type", vechicles.vechiclesTypeId);
            return View(vechicles);
        }

        [Authorize(Roles = "FleetTeam")]
        // GET: vechicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vechicles = await _context.vechicles
                .Include(v => v.vechiclesType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vechicles == null)
            {
                return NotFound();
            }

            return View(vechicles);
        }

        [Authorize(Roles = "FleetTeam")]
        // POST: vechicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vechicles = await _context.vechicles.FindAsync(id);
            _context.vechicles.Remove(vechicles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool vechiclesExists(int id)
        {
            return _context.vechicles.Any(e => e.Id == id);
        }
    }
}
