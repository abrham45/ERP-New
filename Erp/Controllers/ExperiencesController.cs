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
using Erp.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Erp.Controllers
{
    public class ExperiencesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
      
        public ExperiencesController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "HR-Admin")]
        // GET: Experiences
        public async Task<IActionResult> Index()
        {
        
            var employeeContext = _context.Experience.Include(e => e.Employee);
            return View(await employeeContext.ToListAsync());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> IndexBasic()
        {
            User user = await _userManager.GetUserAsync(User);
            var exp = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (exp != null)
            {
                var empall = _context.Experience.Where(e => e.EmployeeId == exp.Id);
                return View(await empall.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: Experiences/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experience
                .Include(e => e.Employee).Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // GET: Experiences/Create
    
        public async Task<IActionResult> Create()
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
            if (emp == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Experience experience)
        {
            if (ModelState.IsValid)
            {
                /*Experience experience = new Experience();*/

                User users = await _userManager.GetUserAsync(User);
                var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);
                if (emp == null)
                {
                    return NotFound();
                }
                experience.EmployeeId = emp.Id;
                experience.UserId = users.Id;
                experience.reason = Convert.ToString(HttpContext.Request.Form["reason"]);
                experience.sentDate = DateTime.Today;
                experience.status = null;
                _context.Add(experience);
                await _context.SaveChangesAsync();
               return RedirectToAction(nameof(IndexBasic));
            }
          
            return View(experience);
        }

        // GET: Experiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }
         
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approved (int id)
        {
          
            var experience = await _context.Experience.FindAsync(id);

            if (experience == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    experience.status = true;
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.Id))
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
        public async Task<IActionResult> Reject(int id)
        {
            var experience = await _context.Experience.FindAsync(id);

            if (experience == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    experience.status = false;
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.Id))
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
 
        // GET: Experiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experience
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }
        [Authorize(Roles = "HR-Admin")]
        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experience = await _context.Experience.FindAsync(id);
            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
            return _context.Experience.Any(e => e.Id == id);
        }
    }
}
