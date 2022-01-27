using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class RegionPlan
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int FiscalYear { get; set; }
        public int Period { get; set; }
        public string Description { get; set; }
        public int ProjectNumber { get; set; }
        public bool? Status { get; set; }
        public decimal OperationExpense { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
