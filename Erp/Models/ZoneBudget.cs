using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class ZoneBudget
    {
        public int Id { get; set; }
       
        public string Description { get; set; }
        public double OverheadCosts { get; set; }
        public double TimeCosts { get; set; }
        public double ExternalCost { get; set; }
        public double ZoneTotalCost { get; set; }
        public bool? Status { get; set; }
        public int ZonePlanId { get; set; }
        public ZonePlan ZonePlan { get; set; }
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }
    }
}
