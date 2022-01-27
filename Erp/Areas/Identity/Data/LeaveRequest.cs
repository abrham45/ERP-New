using Erp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Areas.Identity.Data
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("LeaveTypeId")]
        public string Reason { get; set; }
        public string? Feedback { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime? DateActioned { get; set; }
        public bool? Approved { get; set; }  
        public int EmployeeId { get; set; }
        public int ApprovedById { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public bool hasRequested { get; set; } = false;



    }
}
