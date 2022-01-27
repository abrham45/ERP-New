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
    public class PositionsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public PositionsController(EmployeeContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Authorities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Position.ToListAsync());
        }

        // GET: Authorities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Position = await _context.Position
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Position == null)
            {
                return NotFound();
            }

            return View(Position);
        }

        // GET: Authorities/Create
        public IActionResult Create()
        {
            /*User users = await _userManager.GetUserAsync(User);
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {*/
                return View();
           // }
        }

        // POST: Authorities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Summary")] Position Position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Position);
        }

        // GET: Authorities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Position = await _context.Position.FindAsync(id);
            if (Position == null)
            {
                return NotFound();
            }
            return View(Position);
        }

        // POST: Authorities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Summary")] Position Position)
        {
            if (id != Position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(Position.Id))
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
            return View(Position);
        }

        // GET: Authorities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Position = await _context.Position
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Position == null)
            {
                return NotFound();
            }

            return View(Position);
        }

        // POST: Authorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Position = await _context.Position.FindAsync(id);
            _context.Position.Remove(Position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return _context.Position.Any(e => e.Id == id);
        }
    }
}
