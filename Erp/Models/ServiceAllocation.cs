using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class ServiceAllocation
    {
        public int Id { get; set; }
        public int DriverAllocationId { get; set; }
        public int EmployeeId { get; set; }

        public DriverAllocation DriverAllocation { get; set; }
        public Employee Employee { get; set; }
    }

}
