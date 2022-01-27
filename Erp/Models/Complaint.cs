using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int DepartmentId { get; set; }

        public int? DriverId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime SentDate { get; set; }
        public Driver Drivers { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        public bool? status { get; set; }

        public Employee Employee { get; set; }
    }
}
