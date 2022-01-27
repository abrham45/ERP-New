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
    public class FaultyAssetTypesController : Controller
    {
        private readonly EmployeeContext _context;

        public FaultyAssetTypesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: FaultyAssetTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FaultyAssetType.ToListAsync());
        }

        // GET: FaultyAssetTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultyAssetType = await _context.FaultyAssetType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faultyAssetType == null)
            {
                return NotFound();
            }

            return View(faultyAssetType);
        }

        // GET: FaultyAssetTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FaultyAssetTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FaultyType,Reason")] FaultyAssetType faultyAssetType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faultyAssetType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faultyAssetType);
        }

        // GET: FaultyAssetTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultyAssetType = await _context.FaultyAssetType.FindAsync(id);
            if (faultyAssetType == null)
            {
                return NotFound();
            }
            return View(faultyAssetType);
        }

        // POST: FaultyAssetTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FaultyType,Reason")] FaultyAssetType faultyAssetType)
        {
            if (id != faultyAssetType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faultyAssetType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultyAssetTypeExists(faultyAssetType.Id))
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
            return View(faultyAssetType);
        }

        // GET: FaultyAssetTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultyAssetType = await _context.FaultyAssetType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faultyAssetType == null)
            {
                return NotFound();
            }

            return View(faultyAssetType);
        }

        // POST: FaultyAssetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faultyAssetType = await _context.FaultyAssetType.FindAsync(id);
            _context.FaultyAssetType.Remove(faultyAssetType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultyAssetTypeExists(int id)
        {
            return _context.FaultyAssetType.Any(e => e.Id == id);
        }
    }
}
