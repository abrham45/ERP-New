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

namespace Erp.Controllers
{
    public class SendFilesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public SendFilesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SendFiles
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);


             if (emp != null)
            {
             
                var send = _context.SendFile.Where(e => e.senderId == emp.Id).Include(e => e.Employee).OrderByDescending(s => s.Id);

              
                return View(await send.ToListAsync());
            }
            else
            {
                return NotFound();
            }


            
        }
        public async Task<IActionResult> IndexTo()
        {
            var empsto = Convert.ToString(HttpContext.Request.Form["ReciverId"]);

            var empto = _context.Employees.FirstOrDefault(e => e.EmployeeCode == empsto);
            if (empto != null)
            {

                var res = _context.SendFile.Where(e => e.ReciverId == empto.Id).Include(e => e.Employee).OrderByDescending(s => s.Id);


                return View(await res.ToListAsync());
            }
            else
            {
                return NotFound();
            }


         
        }


        // GET: SendFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendFile = await _context.SendFile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sendFile == null)
            {
                return NotFound();
            }

            return View(sendFile);
        }

        // GET: SendFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SendFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,path,senderId,ReciverId,DateTime,filesatuts")] SendFile sendFile)
        {
            User users = await _userManager.GetUserAsync(User);
            var empfrom = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (sendFile.path !=null)
            {

                var employeecode = Convert.ToString(HttpContext.Request.Form["EmployeeId"]);
                var empto = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employeecode);

                if (empto !=null & empfrom !=null)
                {
                    sendFile.senderId = empfrom.Id;
                    sendFile.ReciverId = empto.Id;
                    sendFile.filesatuts = null;

                    _context.Add(sendFile);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["Error"] = " Invalid Employee code";
                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                TempData["Error"] = " Invalid File Path";
                return RedirectToAction(nameof(Create));
            }

            


        }

        // GET: SendFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendFile = await _context.SendFile.FindAsync(id);
            if (sendFile == null)
            {
                return NotFound();
            }
            return View(sendFile);
        }

        // POST: SendFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,path,senderId,ReciverId,DateTime,filesatuts")] SendFile sendFile)
        {
            if (id != sendFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sendFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SendFileExists(sendFile.Id))
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
            return View(sendFile);
        }

        // GET: SendFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendFile = await _context.SendFile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sendFile == null)
            {
                return NotFound();
            }

            return View(sendFile);
        }

        // POST: SendFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sendFile = await _context.SendFile.FindAsync(id);
            _context.SendFile.Remove(sendFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SendFileExists(int id)
        {
            return _context.SendFile.Any(e => e.Id == id);
        }
    }
}
