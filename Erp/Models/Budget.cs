using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        public int DirectorPlanId { get; set; }
        public string Description { get; set; }
        public double OverheadCosts { get; set; }
        public double TimeCosts { get; set; }
        public double ExternalCost { get; set; }
        public double TotalCost { get; set; }
        public bool? Status { get; set; }

        public int EmployeeId { get; set; }
        public DirectorPlan DirectorPlan { get; set; }

        public Employee Employee { get; set; }

    }
}
