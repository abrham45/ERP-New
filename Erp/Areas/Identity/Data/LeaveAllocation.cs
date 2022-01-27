using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Areas.Identity.Data
{
    public class LeaveAllocation
    {

        public int Id { get; set; }

        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }

        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }

        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
