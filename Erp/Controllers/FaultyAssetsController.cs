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
using Microsoft.AspNetCore.Identity;
using Erp.Areas.Identity.Data;

namespace Erp.Controllers
{
    public class FaultyAssetsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public FaultyAssetsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Maintainance")]
        public async Task<IActionResult> IndexMain()
        {
            var faultyAsset = from f in _context.FaultyAsset.Include(f => f.Asset).Include(f => f.FaultyAssetType) select f;
            return View(await faultyAsset.ToListAsync());
        }

        [Authorize(Roles = "StoreManager")]
        // GET: FaultyAssets
        public async Task<IActionResult> Index()
        {
            var faultyAsset = from f in _context.FaultyAsset.Include(f => f.Asset).Include(f => f.FaultyAssetType)select f;
            return View(await faultyAsset.ToListAsync());
        }

        // GET: FaultyAssets/Details/5
        [Authorize(Roles = "StoreManager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultyAsset = await _context.FaultyAsset
                .Include(f => f.Asset)
                .Include(f => f.FaultyAssetType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faultyAsset == null)
            {
                return NotFound();
            }

            return View(faultyAsset);
        }

        // GET: FaultyAssets/Create
        [Authorize(Roles = "StoreManager")]
        public IActionResult Create()
        {
            ViewBag.FaultyAssetTypeId = new SelectList(_context.FaultyAssetType, "Id", "FaultyType");
            return View();
        }
        [Authorize(Roles = "StoreManager")]
        // POST: FaultyAssets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reason,AssetId,FaultyAssetTypeId")] FaultyAsset faultyAsset)
        {
            var factNum = Convert.ToString(HttpContext.Request.Form["AssetId"]);
            var asset = _context.Asset.FirstOrDefault(e => e.factory_number == factNum);

            if (asset != null)
            {

                faultyAsset.AssetId = asset.Id;
                _context.Add(faultyAsset);
                await _context.SaveChangesAsync();
                ViewBag.FaultyAssetTypeId = new SelectList(_context.FaultyAssetType, "Id", "FaultyType", faultyAsset.FaultyAssetTypeId);
                return RedirectToAction(nameof(Index));
            }
            else
            {

                TempData["Error"] = " Invalid Asset or Asset Factory Number";
                ViewBag.FaultyAssetTypeId = new SelectList(_context.FaultyAssetType, "Id", "FaultyType", faultyAsset.FaultyAssetTypeId);
                return RedirectToAction(nameof(Create));
            }

      
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approved(int id)
        {

            var faultyAsset = await _context.FaultyAsset.FindAsync(id);

            if (faultyAsset == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    faultyAsset.status = true;
                    _context.Update(faultyAsset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultyAssetExists(faultyAsset.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexMain));
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reject(int id)
        {
            var faultyAsset = await _context.FaultyAsset.FindAsync(id);

            if (faultyAsset == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    faultyAsset.status = false;
                    _context.Update(faultyAsset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultyAssetExists(faultyAsset.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexMain));
            }
        }
        /// <summary>
        /// ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Assign(int id)
        {
            var faultyAsset = await _context.FaultyAsset.FindAsync(id);


            if (faultyAsset == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    /*var employees = _context.Employees.Where();*/
                    faultyAsset.status = false;
                    _context.Update(faultyAsset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultyAssetExists(faultyAsset.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexMain));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<IActionResult> DetailsMain(int? id)
        {
            User user = await _userManager.GetUserAsync(User);


            if (id == null)
            {
                return NotFound();
            }

            var faultyAsset = await _context.FaultyAsset
                .Include(f => f.Asset)
                .Include(f => f.FaultyAssetType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faultyAsset == null)
            {
                return NotFound();
            }

            return View(faultyAsset);
        }



        // GET: FaultyAssets/Edit/5
        [Authorize(Roles = "StoreManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultyAsset = await _context.FaultyAsset.FindAsync(id);
            if (faultyAsset == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Asset_Name", faultyAsset.AssetId);
            
            ViewData["FaultyAssetTypeId"] = new SelectList(_context.FaultyAssetType, "Id", "Id", faultyAsset.FaultyAssetTypeId);
            return View(faultyAsset);
        }

        // POST: FaultyAssets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reason,AssetId,FaultyAssetTypeId,EmployeeId")] FaultyAsset faultyAsset)
        {
            if (id != faultyAsset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faultyAsset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultyAssetExists(faultyAsset.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "Asset_Name", faultyAsset.AssetId);
           
            ViewData["FaultyAssetTypeId"] = new SelectList(_context.FaultyAssetType, "Id", "Id", faultyAsset.FaultyAssetTypeId);
            return View(faultyAsset);
        }

        // GET: FaultyAssets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultyAsset = await _context.FaultyAsset
                .Include(f => f.Asset)
                
                .Include(f => f.FaultyAssetType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faultyAsset == null)
            {
                return NotFound();
            }

            return View(faultyAsset);
        }

        // POST: FaultyAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faultyAsset = await _context.FaultyAsset.FindAsync(id);
            _context.FaultyAsset.Remove(faultyAsset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultyAssetExists(int id)
        {
            return _context.FaultyAsset.Any(e => e.Id == id);
        }
    }
}
