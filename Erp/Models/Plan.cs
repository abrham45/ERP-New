using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int FiscalYear { get; set; }
        public int PlanTypeId { get; set; }
        public int Period { get; set; }
        public string Description { get; set; }
        public int ProjectNumber { get; set; }
        public bool? Status { get; set; }
        public decimal OperationExpense { get; set; }
      /*  public DateTime EffectiveDate { get; set; }*/
        public int? EmployeeId { get; set; }
        public int? DepartmentId { get; set; }

        public Employee Employee { get; set; }
        public PlanType PlanType { get; set; }
        public Department Department { get; set; }

    }
}
