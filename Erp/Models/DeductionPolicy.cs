using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class DeductionPolicy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Decimal Amount { get; set; }
        public DateTime Date { get; set; }
      

    }
}
