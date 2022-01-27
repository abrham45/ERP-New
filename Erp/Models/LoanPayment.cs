using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class LoanPayment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal PerMounthAmount { get; set; }
        public string Status { get; set; }
        public DateTime DeductionDate { get; set; }
        public int EmployeeId { get; set; }
        public int LoanSchemeId { get; set; }

        public Employee Employee { get; set; }
        public LoanRequest LoanScheme { get; set; }


    }
}
