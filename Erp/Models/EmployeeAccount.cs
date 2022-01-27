using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class EmployeeAccount
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Decimal OrgEobiAmount { get; set; }
        public Decimal SalaryEobiAmount { get; set; }
        public Decimal SalaryMedicalAmount { get; set; }
        public Decimal OrgInsuranceAmount { get; set; }
        public Decimal SalaryInsuranceAmount { get; set; }
        public Decimal TotalEobiAmount { get; set; }
        public Decimal TotalInsuranceAmount { get; set; }
        public Decimal TotalMedicalAmount { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
