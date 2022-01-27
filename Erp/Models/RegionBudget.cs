using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class RegionBudget
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double TotalRegionOverheadCosts { get; set; }  // sum of all zones OverheadCosts
        public double TotalRegionTimeCosts { get; set; }     // sum of all zones  TimeCosts
        public double TotalRegionExternalCost { get; set; }   // sum of all zones ExternalCost
        public double TotalRegionZoneTotalCost { get; set; }   // sum of all zones TotalCost
        public bool? Status { get; set; }
        public int RegionPlanId { get; set; }
        public RegionPlan RegionPlan { get; set; }
        public int RegionId { get; set; }
        public int ZoneBudgetId { get; set; }
        public ZoneBudget ZoneBudget { get; set; }
        public Region Region { get; set; }
    }
}
