using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Termination
    {
        [Key]
        public int Id { get; set; }
        public string Reason { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
