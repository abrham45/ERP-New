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


    /*[Authorize(Roles = "SuperAdmin")]*/
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        /* private readonly <User> _userManager;
 */
        private readonly EmployeeContext _context;
        public PermissionController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, EmployeeContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string userid)
        {

            var user = _context.UserRoles.FirstOrDefault(q => q.UserId == userid);
            var roleId = user.RoleId;
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions.Employee), roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            //model.UserId = user.UserId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            return View(model);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Update(string userid,PermissionViewModel model)
        {
            var user = _context.UserRoles.FirstOrDefault(q => q.UserId == userid);
            var roleId = user.RoleId;
            var role = await _roleManager.FindByIdAsync(roleId);
          
            
            
            
            var claims = await _roleManager.GetClaimsAsync(role);
            
            
            
            var roleclaim = _context.RoleClaims.FirstOrDefault(q => q.RoleId == role.Id);
           /* var claims = _context.UserClaims(role);*/

            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
                
            }

            return RedirectToAction("Index", new { userid = model.UserId});
        }
    }
}