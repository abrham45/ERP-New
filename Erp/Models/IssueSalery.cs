using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class IssueSalery
    {
        [Key]
        public int Id { get; set; }
        public DateTime IssueDate { get; set; } //system date
        public bool? Status { get; set; }
        public Decimal NetAmount { get; set; } //Net_Amount = (Total_ Salary * Tax)+Total_Monthly_Allowance - Total_Monthly_deduction
        public Decimal? TotalDeduction { get; set; } //Total_Monthly_deduction = Total _decuction + Total_Loan_Monthly_Decuction
        public Decimal? TotalAlllowance { get; set; }

        public int EmployeeId { get; set; }
        public int? LoanRequestId { get; set; }
        public int? EmployeeBonusId { get; set; }
        public int? EmployeeAllowanceId { get; set; }
        public int SalaryId { get; set; }

      
        public Employee Employee { get; set; }
        public LoanRequest LoanRequest { get; set; }
        public Salary Salary { get; set; }
        public EmployeeBonus EmployeeBonus { get; set; }
        public EmployeeAllowance EmployeeAllowance { get; set; }

    }
}
