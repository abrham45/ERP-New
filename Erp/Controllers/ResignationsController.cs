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

namespace Erp.Controllers
{
    public class ResignationsController : Controller
    {
        private readonly EmployeeContext _context;

        private readonly UserManager<User> _userManager;

        public ResignationsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Resignations
     
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (assetal != null)
            {
                var empall = _context.Resignation.Where(e => e.EmployeeId == assetal.Id);
                return View(await empall.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> IndexDriver()
        {
            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Driver.FirstOrDefault(a => a.UserId == user.Id);
            if (assetal != null)
            {
                var roc = _context.Resignation.Where(e => e.DriverId == assetal.Id);
                return View(await roc.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "Director,HR-Admin")]
        public async Task<IActionResult> IndexDir()
        {

            User user = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (emp != null)
            {
                var complaint = _context.Resignation.Where(q => q.EmployeeId == emp.TeamId).ToList();

                return View(complaint);
            }
            else
            {
                return NotFound();
            }

        }
    

        // GET: Resignations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resignation = await _context.Resignation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resignation == null)
            {
                return NotFound();
            }

            return View(resignation);
        }


        // GET: Resignations/Create

        public ActionResult Create()
        {
            return View();
        }
        // POST: Resignations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reason,UserId,EmployeeId,status")] Resignation resignation)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
            var dive = _context.Driver.FirstOrDefault(e => e.UserId == users.Id);
            if (emp != null & dive==null)
            {

                resignation.EmployeeId = emp.Id;
                resignation.status = null;

                resignation.Date = DateTime.Today;


                _context.Add(resignation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

        }
        else if(dive!=null & emp==null)

            {
                resignation.DriverId = dive.Id;
                resignation.status = null;

                resignation.Date = DateTime.Today;


                _context.Add(resignation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexDriver));

            }
          return View(resignation);
    }


    [Authorize(Roles = "Director")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {

            var resignation_req = await _context.Resignation.FindAsync(id);

        if (resignation_req == null)
        {
            return NotFound();
        }
        else
        {
            try
            {
                resignation_req.status = true;
                _context.Update(resignation_req);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResignationExists(resignation_req.Id))
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Reject(int id)
    {
        var resignation_req = await _context.Resignation.FindAsync(id);

        if (resignation_req == null)
        {
            return NotFound();
        }
        else
        {
            try
            {
                    resignation_req.status = false;
                _context.Update(resignation_req);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResignationExists(resignation_req.Id))
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


    // GET: Resignations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var assetRequest = await _context.AssetRequest.FindAsync(id);
        if (assetRequest == null)
        {
            return NotFound();
        }
        return View(assetRequest);
    }

    // POST: Resignations/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reason,UserId,EmployeeId,status")] Resignation resignation)
        {
            if (id != resignation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resignation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResignationExists(resignation.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Country", resignation.EmployeeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", resignation.UserId);
            return View(resignation);
        }

        // GET: Resignations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resignation = await _context.Resignation
                .Include(r => r.Employee)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resignation == null)
            {
                return NotFound();
            }

            return View(resignation);
        }

        // POST: Resignations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resignation = await _context.Resignation.FindAsync(id);
            _context.Resignation.Remove(resignation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResignationExists(int id)
        {
            return _context.Resignation.Any(e => e.Id == id);
        }
    }
}
