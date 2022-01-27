using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;

namespace Erp.Controllers
{
    public class ZonesController : Controller
    {
        private readonly EmployeeContext _context;

        public ZonesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Zone.Include(z => z.Region).Include(z => z.User);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = await _context.Zone
                .Include(z => z.Region)
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ZoneCode,ZoneName,FirstName,LastName,Sex,Mobile,WorkTelephone,AboutMe,AreaOfExpertise,UserId,RegionId")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", zone.RegionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", zone.UserId);
            return View(zone);
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = await _context.Zone.FindAsync(id);
            if (zone == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", zone.RegionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", zone.UserId);
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ZoneCode,ZoneName,FirstName,LastName,Sex,Mobile,WorkTelephone,AboutMe,AreaOfExpertise,UserId,RegionId")] Zone zone)
        {
            if (id != zone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZoneExists(zone.Id))
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
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "FirstName", zone.RegionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", zone.UserId);
            return View(zone);
        }

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = await _context.Zone
                .Include(z => z.Region)
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zone = await _context.Zone.FindAsync(id);
            _context.Zone.Remove(zone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneExists(int id)
        {
            return _context.Zone.Any(e => e.Id == id);
        }
    }
}
