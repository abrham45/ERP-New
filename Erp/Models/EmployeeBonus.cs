using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class EmployeeBonus
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int BonusId { get; set; }
        public DateTime EffectiveDate { get; set; }

        public Employee Employee { get; set; }
        public BonusPolicy Bonus { get; set; }
    }
}
