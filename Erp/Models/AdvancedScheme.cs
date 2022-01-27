using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AdvancedScheme
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal PercentageAmount { get; set; }
        public decimal DeductionEachMonth { get; set; }
        public string DurationRecover { get; set; }

        public int EmployeeId { get; set; }   //fk
        public int SalarId{ get; set; }  //fk

        public Employee Employee { get; set; }   //fk
        public BasicSalary BasicSalary { get; set; }   //fk
    }
}
