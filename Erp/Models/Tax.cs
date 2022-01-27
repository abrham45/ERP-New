using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public Decimal MinAmount { get; set; }
        public Decimal MaxAmount { get; set; }
        public Decimal TaxPercent { get; set; }
  

    }
}
