using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public Decimal GrossSalary { get; set; }
        public int EmployeeId { get; set; } //employee Id
       /* public int TaxId { get; set; } //tax Id*/
        public decimal TaxedSalary { get; set; }


        public Employee Employee { get; set; }
        /*public Tax Tax { get; set; }*/
    }
}
