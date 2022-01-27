using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Assign
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Employee))]
        public int EmpolyeeId { get; set; }
        [ForeignKey(nameof(Tasks))]
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public Decimal? Status { get; set; }
        public int FromEmp { get; set; }

        public Employee Employee { get; set; }
        public Tasks Tasks { get; set; }
    }
}
