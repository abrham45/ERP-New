using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class EmployeeAllocation
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }

        public vechicles vechicles { get; set; }
        public Employee Employee { get; set; }
    }
}
