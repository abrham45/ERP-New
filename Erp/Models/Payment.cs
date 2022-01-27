using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

    }
}
