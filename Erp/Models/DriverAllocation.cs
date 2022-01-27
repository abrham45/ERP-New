using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class DriverAllocation
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
        public int? RouteId { get; set; }
        public Driver Driver { get; set; }

        public vechicles Vehicle { get; set; }
        public Route Route { get; set; }

    }
}
