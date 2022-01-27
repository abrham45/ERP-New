using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Route
    {
        [Key]
        public int Id{ get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public decimal Distance { get; set; }
        public decimal Expenses { get; set; }

    }
}
