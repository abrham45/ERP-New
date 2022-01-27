using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Erp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Http;
using Erp.Data;
using Erp.Models;

namespace Erp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmployeeContext _context;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
             EmployeeContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public InputEmpModel InputEmp { get; set; }
        public string UserNameChangeLimitMessage { get; set; }
        /// <summary>
        /// /
        /// </summary>
        public class InputModel
        {
            [Display(Name = "Employee Code")]
            public string EmployeeCode { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }

            [TempData]
            public string StatusMessage { get; set; }
            [TempData]
            public string UserNameChangeLimitMessage { get; set; }
            [BindProperty]
            public InputModel Input { get; set; }

        }
        /// <summary>
        /// 
        /// </summary>
        public class InputEmpModel
        {

            public string EmployeeCode { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
            public string Nationality { get; set; }
            public DateTime Dob { get; set; }
            public string Mobile { get; set; }
            public string HomeTelephone { get; set; }
            public string WorkTelephone { get; set; }
            public int Fax { get; set; }
         
            public string Country { get; set; }
            public string Region { get; set; }
            public string City { get; set; }
            public string Subcity { get; set; }
            public string Woreda { get; set; }
            public string Position { get; set; }
            public bool Status { get; set; }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        private async Task LoadAsync(User user)
        {
            User users = await _userManager.GetUserAsync(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users.Id);

            var userName = await _userManager.GetUserNameAsync(user);
            var profilePicture = user.ProfilePicture;

            Username = userName;
            Input = new InputModel
            {
                
                Username = userName,
                ProfilePicture = profilePicture
            };

            if (emp != null)
            {
                InputEmp = new InputEmpModel
                {
                    EmployeeCode = emp.EmployeeCode,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Sex = emp.Sex,
                    Region = emp.Region,
                    City = emp.City,
                    Mobile = emp.Mobile,
                    Dob = emp.DateOfBirth,
                    HomeTelephone = emp.HomeTelephone,
                    WorkTelephone = emp.WorkTelephone,
                   
                    Country = emp.Country,
                    Subcity = emp.Subcity,
                    Woreda = emp.Woreda,
                    Status = emp.Status,
                };
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            UserNameChangeLimitMessage = $"You can change your username {user.UsernameChangeLimit} more time(s).";
            await LoadAsync(user);
            return Page();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            User user = await _userManager.GetUserAsync(User);

          
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }
           /* await _signInManager.RefreshSignInAsync(user);*/
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
