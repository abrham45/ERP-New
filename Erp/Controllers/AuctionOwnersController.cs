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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Erp.Controllers
{
    public class AuctionOwnersController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public AuctionOwnersController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: AuctionOwners
        [Authorize(Roles="AuctionTeam, Director")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuctionOwner.ToListAsync());
        }

        // GET: AuctionOwners/Details/5

        /// <returns></returns>
        [Authorize(Roles = "AuctionTeam")]
        public async Task<IActionResult> ExportToExcel()
        {
            var emp = from a in _context.AuctionOwner select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Auction");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "AuctionNumber";
                worksheet.Cell(currentRow, 3).Value = "AssetName";
                worksheet.Cell(currentRow, 4).Value = "Description";
                worksheet.Cell(currentRow, 5).Value = "Quantity";
                worksheet.Cell(currentRow, 6).Value = "Specification";
                worksheet.Cell(currentRow, 7).Value = "Date";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.AuctionNumber;
                        worksheet.Cell(currentRow, 3).Value = i.AssetName;
                        worksheet.Cell(currentRow, 4).Value = i.Description;
                        worksheet.Cell(currentRow, 5).Value = i.Quantity;
                        worksheet.Cell(currentRow, 6).Value = i.Specification;
                        worksheet.Cell(currentRow, 7).Value = i.Date;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=Auction.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
        /// <summary>.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionOwner = await _context.AuctionOwner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auctionOwner == null)
            {
                return NotFound();
            }

            return View(auctionOwner);
        }

        // GET: AuctionOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuctionOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuctionNumber,AssetName,Description,Quantity,Specification,Date")] AuctionOwner auctionOwner)
        {
            if (ModelState.IsValid)
            {
               
                    auctionOwner.Date = DateTime.Today;
                   
                    _context.Add(auctionOwner);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
              
            }
            return View(auctionOwner);
        }

        // GET: AuctionOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionOwner = await _context.AuctionOwner.FindAsync(id);
            if (auctionOwner == null)
            {
                return NotFound();
            }
            return View(auctionOwner);
        }

        // POST: AuctionOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuctionNumber,AssetName,Description,Quantity,Specification,Date")] AuctionOwner auctionOwner)
        {
            if (id != auctionOwner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auctionOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionOwnerExists(auctionOwner.Id))
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
            return View(auctionOwner);
        }

        // GET: AuctionOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionOwner = await _context.AuctionOwner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auctionOwner == null)
            {
                return NotFound();
            }

            return View(auctionOwner);
        }

        // POST: AuctionOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auctionOwner = await _context.AuctionOwner.FindAsync(id);
            _context.AuctionOwner.Remove(auctionOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuctionOwnerExists(int id)
        {
            return _context.AuctionOwner.Any(e => e.Id == id);
        }
    }
}
