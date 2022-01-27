using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class ManageUserRolesViewModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
     
        public Employee Employee { get; set; }
        public Department Department { get; set; }

    
    public bool Selected { get; set; }
    }
}

