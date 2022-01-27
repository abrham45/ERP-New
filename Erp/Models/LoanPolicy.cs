using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class LoanPolicy
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Decimal MaxAmount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

    }
}
