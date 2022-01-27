

using Erp.Areas.Identity.Data;
using Erp.Data;
using Erp.Enums;
using Erp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmployeeContext _context;


        public UserRolesController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, EmployeeContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index(string searchTerm)
        {
            //var currentUser = await _userManager.Users.ToListAsync();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var users = await _userManager.Users.Where(u => u.Id != currentUser.Id).ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
         
            foreach (User user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.DepartmentId = user.DepartmentId;
                thisViewModel.Roles = await GetUserRoles(user);
                if (!String.IsNullOrEmpty(searchTerm))
                {
                    userRolesViewModel = userRolesViewModel.Where(s => s.FirstName.Contains(searchTerm)).ToList();
                }
                userRolesViewModel.Add(thisViewModel);
            }
           
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }


        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;


            var user = await _userManager.FindByIdAsync(userId);
            var userEmp = _context.Employees.FirstOrDefault(a => a.UserId == userId);
            var userrole = _context.Users.FirstOrDefault(a => a.Id == userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,



                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    /* if (role.Name == "Director")
                     {
                         userRolesViewModel.DepartmentId = userEmp.DepartmentId;

                     }*/

                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }


                model.Add(userRolesViewModel);

            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            var userrole = _context.Users.FirstOrDefault(a => a.Id == userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }


            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            var userAsp = _context.UserRoles.FirstOrDefault(a => a.UserId == user.Id);

            userrole.RoleId = userAsp.RoleId;




            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }


            //_context.Users.Update(userrole);
            //await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //  public bool UpdateUser(User model, string userId)
        //   {

        // var userAsp = _context.UserRoles.FirstOrDefault(a => a.UserId == user.Id);
        //  var user = _context.Users.FirstOrDefault(a => a.UserId == model);

        //  user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;

        //  _context.Users.Update(user);
        //  _context.SaveChanges();

        // return true;
        // }
    }
}
