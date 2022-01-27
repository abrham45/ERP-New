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
    public class AssetRequestsController : Controller
    {
        private readonly EmployeeContext _context;
      
        private readonly UserManager<User> _userManager;

        public AssetRequestsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AssetRequests
      
        public async Task<IActionResult> Index(int pageNumber = 1)
        {

            User user = await _userManager.GetUserAsync(User);
            var assetal = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);
            if (assetal != null)
            {
                var empall = _context.AssetRequest.Where(e => e.EmployeeId == assetal.Id);
                return View(await PaginatedList<AssetRequest>.CreateAsync(empall, pageNumber, 7));
               
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Director")]
        public async Task<IActionResult> IndexDir()
        {

            var users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.Where(e => e.DepartmentId == users.DepartmentId).Select(x => x.Id).ToList();


            var assetRequests = _context.AssetRequest.Include(e=> e.Employee).Where( e => emp.Contains(e.EmployeeId)).ToList();


            return View(assetRequests);
         
        }

        [Authorize(Roles = "Invertory")]
        public async Task<IActionResult> IndexInv(int pageNumber=1)
        {

            return View(await PaginatedList<AssetRequest>.CreateAsync(_context.AssetRequest, pageNumber, 7));

         
        }


        // GET: AssetRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetRequest = await _context.AssetRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetRequest == null)
            {
                return NotFound();
            }

            return View(assetRequest);
        }

        // GET: AssetRequests/Create
     
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Asset,EmployeeId,RequestedDate,Description,Approved")] AssetRequest assetRequest)
        {

            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (emp != null)
            {

                assetRequest.EmployeeId = emp.Id;
                assetRequest.Approved = null;
                assetRequest.Asset = Convert.ToString(HttpContext.Request.Form["Asset"]);
                assetRequest.RequestedDate = DateTime.Today;
              

                _context.Add(assetRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assetRequest);
        }

        [Authorize(Roles = "Director")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approved(int id)
        {

            var asset_req = await _context.AssetRequest.FindAsync(id);

            if (asset_req == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    asset_req.Approved = true;
                    _context.Update(asset_req);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetRequestExists(asset_req.Id))
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
            var asset_req = await _context.AssetRequest.FindAsync(id);

            if (asset_req == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    asset_req.Approved = false;
                    _context.Update(asset_req);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetRequestExists(asset_req.Id))
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
        // GET: AssetRequests/Edit/5
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

        // POST: AssetRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssetId,EmployeeId,RequestedDate,Approved,ApprovedById")] AssetRequest assetRequest)
        {
            if (id != assetRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetRequestExists(assetRequest.Id))
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
            return View(assetRequest);
        }

        // GET: AssetRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetRequest = await _context.AssetRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetRequest == null)
            {
                return NotFound();
            }

            return View(assetRequest);
        }

        // POST: AssetRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetRequest = await _context.AssetRequest.FindAsync(id);
            _context.AssetRequest.Remove(assetRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetRequestExists(int id)
        {
            return _context.AssetRequest.Any(e => e.Id == id);
        }
    }
}
