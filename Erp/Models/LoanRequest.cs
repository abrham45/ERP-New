using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class LoanRequest
    {
        [Key]
        public int Id { get; set; }
        public bool? Status { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal EachMonthDeductionAmount { get; set; }
        public DateTime Date { get; set; } //system date
        public decimal TotalLoanAmount { get; set; }
        public decimal LeftLoanAmount { get; set; }
        public int LoanPolicyId { get; set; } //fk
        public int EmployeeId { get; set; }    //fk

        public LeaveRequestVM leaveRequest { get; set; }
        public Employee Employee { get; set; }
        public LoanPolicy LoanPolicy { get; set; }

    }
}
