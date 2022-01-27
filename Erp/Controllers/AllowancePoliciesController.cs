using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Erp.Data;
using Erp.Models;

namespace Erp.Controllers
{
    public class AllowancePoliciesController : Controller
    {
        private readonly EmployeeContext _context;

        public AllowancePoliciesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: AllowancePolicies
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllowancePolicy.ToListAsync());
        }

        // GET: AllowancePolicies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancePolicy = await _context.AllowancePolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allowancePolicy == null)
            {
                return NotFound();
            }

            return View(allowancePolicy);
        }

        // GET: AllowancePolicies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllowancePolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Date,Amount")] AllowancePolicy allowancePolicy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allowancePolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allowancePolicy);
        }

        // GET: AllowancePolicies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancePolicy = await _context.AllowancePolicy.FindAsync(id);
            if (allowancePolicy == null)
            {
                return NotFound();
            }
            return View(allowancePolicy);
        }

        // POST: AllowancePolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Date,Amount")] AllowancePolicy allowancePolicy)
        {
            if (id != allowancePolicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allowancePolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllowancePolicyExists(allowancePolicy.Id))
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
            return View(allowancePolicy);
        }

        // GET: AllowancePolicies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowancePolicy = await _context.AllowancePolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allowancePolicy == null)
            {
                return NotFound();
            }

            return View(allowancePolicy);
        }

        // POST: AllowancePolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allowancePolicy = await _context.AllowancePolicy.FindAsync(id);
            _context.AllowancePolicy.Remove(allowancePolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllowancePolicyExists(int id)
        {
            return _context.AllowancePolicy.Any(e => e.Id == id);
        }
    }
}
