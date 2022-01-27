using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class BasicSalary
    {
        [Key]
        public int BasicSalaryId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalSalary { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
