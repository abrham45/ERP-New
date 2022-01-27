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
    public class vechiclesTypesController : Controller
    {
        private readonly EmployeeContext _context;

        public vechiclesTypesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: vechiclesTypes
        [Authorize(Roles = "FleetTeam")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.vechiclesType.ToListAsync());
        }
        [Authorize(Roles = "FleetTeam")]
        // GET: vechiclesTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vechiclesType = await _context.vechiclesType
                .FirstOrDefaultAsync(m => m.id == id);
            if (vechiclesType == null)
            {
                return NotFound();
            }

            return View(vechiclesType);
        }
        [Authorize(Roles = "FleetTeam")]
        // GET: vechiclesTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "FleetTeam")]
        // POST: vechiclesTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Type,description")] vechiclesType vechiclesType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vechiclesType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vechiclesType);
        }
        [Authorize(Roles = "FleetTeam")]
        // GET: vechiclesTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vechiclesType = await _context.vechiclesType.FindAsync(id);
            if (vechiclesType == null)
            {
                return NotFound();
            }
            return View(vechiclesType);
        }

        [Authorize(Roles = "FleetTeam")]
        // POST: vechiclesTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Type,description")] vechiclesType vechiclesType)
        {
            if (id != vechiclesType.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vechiclesType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!vechiclesTypeExists(vechiclesType.id))
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
            return View(vechiclesType);
        }

        [Authorize(Roles = "FleetTeam")]
        // GET: vechiclesTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vechiclesType = await _context.vechiclesType
                .FirstOrDefaultAsync(m => m.id == id);
            if (vechiclesType == null)
            {
                return NotFound();
            }

            return View(vechiclesType);
        }

        [Authorize(Roles = "FleetTeam")]
        // POST: vechiclesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vechiclesType = await _context.vechiclesType.FindAsync(id);
            _context.vechiclesType.Remove(vechiclesType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool vechiclesTypeExists(int id)
        {
            return _context.vechiclesType.Any(e => e.id == id);
        }
    }
}
