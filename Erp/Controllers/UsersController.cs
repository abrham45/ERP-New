using Erp.Areas.Identity.Data;
using Erp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly EmployeeContext _context;
    /*    private readonly RoleManager<IdentityRole> _roleManager;
        private IPasswordHasher<User> passwordHasher;*/

        public UsersController(UserManager<User> userManager, EmployeeContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser =  _userManager.Users.Where(a => a.Id != currentUser.Id);
            return View(await PaginatedList<User>.CreateAsync(allUsersExceptCurrentUser, pageNumber, 10));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        public async Task<IActionResult> AllUsers(string id, int pageNumber = 1)
        {
            var users = from m in _context.Users select m;
            /*var confirmed_email = _context.Users.FirstOrDefault(q => q.EmailConfirmed == true );*/

            if (!String.IsNullOrEmpty(id))
            {
                users = users.Where(s => s.Id.Contains(id));
            }


            return View(await PaginatedList<User>.CreateAsync(users, pageNumber, 10));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {

            var user = await _context.Users.FindAsync(id);


            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id={id} cannot be found";
                return View("Not Found");
            }



            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, [Bind("Id,FirstName,LastName,Email,EmployeeID,PhoneNumber,UserName,EmailConfirmed")] User user)
        {
            // var user = await _context.Users.FindAsync(id);

            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserviewModelExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AllUsers));
            }


            return View(user);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Departments/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeactivateConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
  
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.EmailConfirmed)
                    {
                        user.EmailConfirmed = false;
                    }
                    else 
                    {
                        user.EmailConfirmed = true;
                    }

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserviewModelExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AllUsers));
            }

            return View(user);
        }


    private bool UserviewModelExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
