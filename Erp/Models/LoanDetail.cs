using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class LoanDetail
    {
        [Key]
        public int LoanDetailId { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }    //fk

        public Employee Employee { get; set; }
    }
}
