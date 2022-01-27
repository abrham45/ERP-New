using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;
using Erp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Erp.Controllers
{
    public class AuctionMembersController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public AuctionMembersController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AuctionMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuctionMember.ToListAsync());
        }

        // GET: AuctionOwners/Details/5

        /// <returns></returns>
        [Authorize(Roles = "AuctionTeam")]
        public async Task<IActionResult> ExportToExcel()
        {
            var emp = from a in _context.AuctionMember select a;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AuctionParticipants");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "AuctionNumber";
                worksheet.Cell(currentRow, 3).Value = "CompanyName";
                worksheet.Cell(currentRow, 4).Value = "Description";
                worksheet.Cell(currentRow, 5).Value = "Quantity";
                worksheet.Cell(currentRow, 6).Value = "Specification";
                worksheet.Cell(currentRow, 7).Value = "Price";
                worksheet.Cell(currentRow, 8).Value = "TinNumber";
                worksheet.Cell(currentRow, 9).Value = "Status";
                worksheet.Cell(currentRow, 10).Value = "Date";

                foreach (var i in emp)
                {
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = i.Id;
                        worksheet.Cell(currentRow, 2).Value = i.AuctionNumber;
                        worksheet.Cell(currentRow, 3).Value = i.CompanyName;
                        worksheet.Cell(currentRow, 4).Value = i.Description;
                        worksheet.Cell(currentRow, 5).Value = i.Quantity;
                        worksheet.Cell(currentRow, 6).Value = i.Specification;
                        worksheet.Cell(currentRow, 7).Value = i.Price;
                        worksheet.Cell(currentRow, 8).Value = i.TinNumber;
                        worksheet.Cell(currentRow, 9).Value = i.Status;
                        worksheet.Cell(currentRow, 10).Value = i.Date;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    Response.Clear();
                    Response.Headers.Add("content-disposition", "attachment;filename=AuctionParticipants.xls");
                    Response.ContentType = "application/xls";
                    await Response.Body.WriteAsync(content);
                    Response.Body.Flush();
                }

                return View();

            }
        }
        /// <summary>
        public async Task<IActionResult> IndexUser()
        {
            var users = _userManager.GetUserAsync(User);
            return View(await _context.AuctionMember.ToListAsync());
        }
        // GET: AuctionMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionMember = await _context.AuctionMember
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auctionMember == null)
            {
                return NotFound();
            }

            return View(auctionMember);
        }

        // GET: AuctionMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuctionMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuctionNumber,CompanyName,Description,Quantity,Specification,Price,TinNumber,Status,Date")] AuctionMember auctionMember)
        {
            
                var auctionnum = Convert.ToString(HttpContext.Request.Form["AuctionNumber"]);
                var tin = auctionMember.TinNumber;

                if (auctionnum != null)
                {
                    var autction = _context.AuctionOwner.FirstOrDefault(e => e.AuctionNumber == auctionnum);

                    if (autction != null)
                    {
                         var all = _context.AuctionMember.FirstOrDefault(q => q.AuctionNumber == auctionnum & q.TinNumber == tin);

                            if (all == null)
                            {
                                auctionMember.Status = null;
                                auctionMember.Date = DateTime.Today;
                                _context.Add(auctionMember);
                                await _context.SaveChangesAsync();


                                TempData["Success"] = "You have Sucessfully Applied for Auction";
                                return RedirectToAction(nameof(Create));
                            }
                            else
                            {
                                TempData["Success"] = "you have already applied";
                                return RedirectToAction(nameof(Create));
                            }
                    }
                    else
                    {
                        TempData["Warning"] = "Invalid Auction Number, Check the Auction Number Inserted.";
                        return RedirectToAction(nameof(Create));
                    }
                }
                else
                {
                    TempData["Warning"] = "Please Enter Auction Number.";
                    return RedirectToAction(nameof(Create));
                }

            
            return View(auctionMember);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Approved(int id)
        {

            var AuctionMemberss = await _context.AuctionMember.FindAsync(id);

            if (AuctionMemberss == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    AuctionMemberss.Status = true;
                    _context.Update(AuctionMemberss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (AuctionMemberExists(id))
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

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reject(int id)
        {
            var AuctionMemberss = await _context.AuctionMember.FindAsync(id);

            if (AuctionMemberss == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    AuctionMemberss.Status = false;
                    _context.Update(AuctionMemberss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (AuctionMemberExists(id))
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
        }
        // GET: AuctionMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionMember = await _context.AuctionMember.FindAsync(id);
            if (auctionMember == null)
            {
                return NotFound();
            }
            return View(auctionMember);
        }

        // POST: AuctionMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuctionNumber,CompanyName,Description,Quantity,Specification,TinNumber,Price,Status,Date")] AuctionMember auctionMember)
        {
            if (id != auctionMember.Id)
            {
                return NotFound();
            }

                var auctionnum = Convert.ToString(HttpContext.Request.Form["AuctionNumber"]);

                if (auctionnum != null)
                {
                    var autction = _context.AuctionOwner.FirstOrDefault(e => e.AuctionNumber == auctionnum);

                    if (autction != null)
                    {
                          
                        _context.Update(auctionMember);
                        await _context.SaveChangesAsync();


                        TempData["Warning"] = "You have Sucessfully Updated Auction Application";
                        return RedirectToAction(nameof(Create));
                    }
                    else
                    {
                        TempData["Warning"] = "Invalid Auction Number, Check the Auction Number Inserted.";
                        return RedirectToAction(nameof(Create));
                    }
                }
                else
                {
                    TempData["Warning"] = "Please Enter Auction Number.";
                    return RedirectToAction(nameof(Create));
                }
                  
              
            return View(auctionMember);
        }

        // GET: AuctionMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionMember = await _context.AuctionMember
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auctionMember == null)
            {
                return NotFound();
            }

            return View(auctionMember);
        }

        // POST: AuctionMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auctionMember = await _context.AuctionMember.FindAsync(id);
            _context.AuctionMember.Remove(auctionMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuctionMemberExists(int id)
        {
            return _context.AuctionMember.Any(e => e.Id == id);
        }
    }
}
