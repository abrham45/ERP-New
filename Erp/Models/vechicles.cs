using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class vechicles
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int vechiclesTypeId { get; set; }
        public string PlateNumber { get; set; }
        public bool? IsInsured { get; set; }

        public vechiclesType vechiclesType { get; set; }
    }
}
