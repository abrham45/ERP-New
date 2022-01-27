using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class EmployeeDeduction
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DeductionPolicyId { get; set; }
        public DateTime EffectiveDate { get; set; }

        public DeductionPolicy DeductionPolicy { get; set; }
        public Employee Employee { get; set; }
    }
}
