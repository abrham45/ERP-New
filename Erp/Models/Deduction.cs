using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Deduction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal EobiDeductionAmount { get; set; }
        public decimal InsuranceDeductionAmount { get; set; }
        public decimal LoanDeductionAmount { get; set; }
        public decimal AdvanceDeductionAmount { get; set; }
        public decimal AttendanceDeductionAmount { get; set; }
        public decimal GaurityDeductionAmount { get; set; }
        public int EmployeeId { get; set; }   //fk
        public int SalaryId { get; set; } //
    }
}
