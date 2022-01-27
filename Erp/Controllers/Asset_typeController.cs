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
    public class Asset_typeController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public Asset_typeController(EmployeeContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Asset_type
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
           
            return View(await PaginatedList<Asset_type>.CreateAsync(_context.Asset_type, pageNumber,7));
        }

        // GET: Asset_type/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_type = await _context.Asset_type
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset_type == null)
            {
                return NotFound();
            }

            return View(asset_type);
        }

        // GET: Asset_type/Create
        public async Task<IActionResult> Create()
        {
            
                return View();
            
        }

        // POST: Asset_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Asset_Type,Description")] Asset_type asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset) ;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asset);
        }

        // GET: Asset_type/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_type = await _context.Asset_type.FindAsync(id);
            if (asset_type == null)
            {
                return NotFound();
            }
            return View(asset_type);
        }

        // POST: Asset_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Asset_Type,Description")] Asset_type asset)
        {
            if (id != asset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Asset_typeExists(asset.Id))
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
            return View(asset);
        }

        // GET: Asset_type/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_type = await _context.Asset_type
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset_type == null)
            {
                return NotFound();
            }

            return View(asset_type);
        }

        // POST: Asset_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset_type = await _context.Asset_type.FindAsync(id);
            _context.Asset_type.Remove(asset_type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Asset_typeExists(int id)
        {
            return _context.Asset_type.Any(e => e.Id == id);
        }
    }
}
