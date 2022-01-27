using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class UserRolesViewModel
    {

        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool Status { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
 
        public Employee Employee { get; set; }
        public Department Department { get; set; }


    }
}
