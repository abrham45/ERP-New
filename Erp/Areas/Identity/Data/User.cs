using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.Models;
using Microsoft.AspNetCore.Identity;

namespace Erp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
        public int DepartmentId { get; set; }
        public string RoleId { get; set; }
        public int EmployeeId { get; set; }



      //public Employee Employee { get; set; }
        public List<Employee>Employees { get; set; }
        public List<LeaveRequest>LeaveRequests { get; set; }
     






    }
}
