using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public int vechiclesId { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }
        public string? Feedback { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public vechicles vechicles { get; set; }


    }
}
