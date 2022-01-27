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
    public class OrderAssetsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;
        public OrderAssetsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "TeamLeader")]
        // GET: OrderAssets
        public async Task<IActionResult> Index()
        {
            var users = _userManager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(q => q.UserId == users);

            var employees = _context.Employees.Where(e => e.DepartmentId == emp.DepartmentId).Select(e => e.Id).ToList();
            var order = _context.OrderAsset.Where(e => employees.Contains(e.EmployeeId));

            return View(await order.ToListAsync());
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "StoreManager")]
        public async Task<IActionResult> IndexUser()
        {
            var users = _userManager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(q => q.UserId == users);

            var order = _context.OrderAsset.Where(e => e.EmployeeId == emp.Id);

            return View(await order.ToListAsync());
        }


        // GET: OrderAssets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAsset = await _context.OrderAsset
                .Include(q=>q.Employee)
                .Include(q => q.Asset_type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderAsset == null)
            {
                return NotFound();
            }

            return View(orderAsset);
        }

        // GET: OrderAssets/Create
        public IActionResult Create()
        {
            ViewBag.AssetTypeId = new SelectList(_context.Asset_type, "Id", "Asset_Type");
            return View();
        }

        // POST: OrderAssets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,AssetTypeId,Quantity,EstimatedPrice,EmployeeId,Status,Specification,OrderedDate")] OrderAsset orderAsset)
        {
            if (ModelState.IsValid)
            {
                var users = _userManager.GetUserId(User);
                var emp = _context.Employees.FirstOrDefault(q => q.UserId == users);

                if (emp != null)
                {
                    orderAsset.Status = null;
                    orderAsset.EmployeeId = emp.Id;
                    orderAsset.OrderedDate = DateTime.Today;

                    _context.Add(orderAsset);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Order is Successfull.";
                    return RedirectToAction(nameof(Create));
                }
                else
                {
                    TempData["Warning"] = "Employee Not found";
                    return RedirectToAction(nameof(Create));
                }
            }
            ViewBag.AssetTypeId = new SelectList(_context.Asset_type, "Id", "Asset_Type", orderAsset.AssetTypeId);
            return View(orderAsset);
        }

        [Authorize(Roles = "StoreManager")]
        // GET: OrderAssets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAsset = await _context.OrderAsset.FindAsync(id);
            if (orderAsset == null)
            {
                return NotFound();
            }
            ViewBag.AssetTypeId = new SelectList(_context.Asset_type, "Id", "Asset_Type", orderAsset.AssetTypeId);
            return View(orderAsset);
        }

        // POST: OrderAssets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,AssetTypeId,Quantity,EstimatedPrice,EmployeeId,Status,Specification,OrderedDate")] OrderAsset orderAsset)
        {
            if (id != orderAsset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderAsset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAssetExists(orderAsset.Id))
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
            ViewBag.AssetTypeId = new SelectList(_context.Asset_type, "Id", "Asset_Type", orderAsset.AssetTypeId);
            return View(orderAsset);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       [Authorize(Roles = "TeamLeader")]
        public async Task<IActionResult> Approved(int id)
        {

            var orderAssets = await _context.OrderAsset.FindAsync(id);

            if (orderAssets == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    orderAssets.Status = true;
                    _context.Update(orderAssets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (OrderAssetExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Attendance");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader")]
        public async Task<IActionResult> Reject(int id)
        {
            var orderAssets = await _context.OrderAsset.FindAsync(id);

            if (orderAssets == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    orderAssets.Status = false;
                    _context.Update(orderAssets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (OrderAssetExists(id))
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
        // GET: OrderAssets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAsset = await _context.OrderAsset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderAsset == null)
            {
                return NotFound();
            }

            return View(orderAsset);
        }

        // POST: OrderAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderAsset = await _context.OrderAsset.FindAsync(id);
            _context.OrderAsset.Remove(orderAsset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderAssetExists(int id)
        {
            return _context.OrderAsset.Any(e => e.Id == id);
        }
    }
}
