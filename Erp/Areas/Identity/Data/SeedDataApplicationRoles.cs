using Erp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Erp.Areas.Identity.Data
{
    public static class SeedDataApplicationRoles
    {
        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Basic.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Director.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Inventory.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.StoreManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.PropertyManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Maintainance.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.TeamLeader.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Supervisor.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.FinanceTeam.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.AuctionTeam.ToString())); 
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.FleetTeam.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.RegionOSC.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.ZoneOSC.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.GeneralDirector.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Minister.ToString()));
           

        }
        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new User
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Super",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Error1@1");

                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                }

            }
        }

        public static async Task SeedPlanSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new User
            {
                UserName = "Plansuperadmin",
                Email = "Plansuperadmin@gmail.com",
                FirstName = "PlanSuper",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Error1@1");

                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Plansuperadmin.ToString());
                }

            }
        }






    }
}
