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
    public class DepersationsController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;


        public DepersationsController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Depersations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depersation.Include(a => a.Asset).ToListAsync());
        }

        // GET: Depersations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depersation = await _context.Depersation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depersation == null)
            {
                return NotFound();
            }

            return View(depersation);
        }

        // GET: Depersations/Create
        public async Task<IActionResult> Create()
        {
            User users = await _userManager.GetUserAsync(User);
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return View();
            }
        }

        // POST: Depersations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Salvage_Value,Useful_Life_Time,Annual_Depersation_Amount,AssetId")] Depersation depersation)
        {

            var factNum = Convert.ToString(HttpContext.Request.Form["AssetId"]);
            var asset = _context.Asset.FirstOrDefault(e => e.factory_number == factNum);

            if(asset != null)
            {
                var price = asset.Price;
               /* var prices = Convert.ToDecimal(HttpContext.Request.Form["PaymentId"]);
                var price = _context.Payment.FirstOrDefault(e => e.Price == prices);
                    */

                var salNum = Convert.ToDecimal(HttpContext.Request.Form["Salvage_Value"]);
                var lifeTime = Convert.ToDecimal(depersation.Useful_Life_Time);
  
                depersation.AssetId = asset.Id;
                depersation.Salvage_Value = Convert.ToInt32(salNum);
                depersation.Useful_Life_Time = Convert.ToInt32(lifeTime);
                depersation.Annual_Depersation_Amount = (int)((price - salNum) / (lifeTime));

                _context.Add(depersation);
                await _context.SaveChangesAsync();
                return Redirect(nameof(Index));
            }
            else
            {

                TempData["Error"] = " Invalid Asset or Asset Factory Number";
                return RedirectToAction(nameof(Create));
            }


           
        }

        // GET: Depersations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depersation = await _context.Depersation.FindAsync(id);
            if (depersation == null)
            {
                return NotFound();
            }
            return View(depersation);
        }

        // POST: Depersations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Salvage_Value,Useful_Life_Time,Annual_Depersation_Amount,AssetId,PaymentId")] Depersation depersation)
        {
            if (id != depersation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depersation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepersationExists(depersation.Id))
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
            return View(depersation);
        }

        // GET: Depersations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depersation = await _context.Depersation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depersation == null)
            {
                return NotFound();
            }

            return View(depersation);
        }

        // POST: Depersations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depersation = await _context.Depersation.FindAsync(id);
            _context.Depersation.Remove(depersation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepersationExists(int id)
        {
            return _context.Depersation.Any(e => e.Id == id);
        }
    }
}
