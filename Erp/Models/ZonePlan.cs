using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class ZonePlan
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public int FiscalYear { get; set; }
        public int PlanTypeId { get; set; }
        public int Period { get; set; }
        public string Description { get; set; }
        public int ProjectNumber { get; set; }
        public bool? Status { get; set; }
        public decimal OperationExpense { get; set; }
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }
        public PlanType PlanType { get; set; }
     
    }
}
