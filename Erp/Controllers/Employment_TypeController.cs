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

namespace Erp.Controllers
{
    [Authorize]
    public class Employment_TypeController : Controller
    {
        private readonly EmployeeContext _context;

        public Employment_TypeController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employment_Type
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employment_Types.ToListAsync());
        }

        // GET: Employment_Type/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment_Type = await _context.Employment_Types
                .FirstOrDefaultAsync(m => m.Employment_TypeId == id);
            if (employment_Type == null)
            {
                return NotFound();
            }

            return View(employment_Type);
        }

        // GET: Employment_Type/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employment_Type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Employment_TypeId,Name,Descrbtion")] Employment_Type employment_Type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employment_Type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employment_Type);
        }

        // GET: Employment_Type/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment_Type = await _context.Employment_Types.FindAsync(id);
            if (employment_Type == null)
            {
                return NotFound();
            }
            return View(employment_Type);
        }

        // POST: Employment_Type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Employment_TypeId,Name,Descrbtion")] Employment_Type employment_Type)
        {
            if (id != employment_Type.Employment_TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employment_Type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Employment_TypeExists(employment_Type.Employment_TypeId))
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
            return View(employment_Type);
        }

        // GET: Employment_Type/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment_Type = await _context.Employment_Types
                .FirstOrDefaultAsync(m => m.Employment_TypeId == id);
            if (employment_Type == null)
            {
                return NotFound();
            }

            return View(employment_Type);
        }

        // POST: Employment_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employment_Type = await _context.Employment_Types.FindAsync(id);
            _context.Employment_Types.Remove(employment_Type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Employment_TypeExists(int id)
        {
            return _context.Employment_Types.Any(e => e.Employment_TypeId == id);
        }
    }
}
