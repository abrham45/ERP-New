using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class DepartmentChange
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int from { get; set; }
        public int DepartmentId { get; set; }
        public DateTime sentDate { get; set; }
        public bool? status { get; set; }
        public string reason { get; set; }


        public Employee Employee { get; set; }
        public Department Department { get; set; }
        public User User { get; set; }
       


    }
}
