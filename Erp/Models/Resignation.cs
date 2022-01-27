using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Resignation
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public string UserId { get; set; }
        public int? EmployeeId { get; set; }

        public int? DriverId { get; set; }
        public DateTime Date { get; set; }
        public bool? status { get; set; }
        public Employee Employee { get; set; }

        public Driver Drivers { get; set; }

        public User User { get; set; }



    }
}
