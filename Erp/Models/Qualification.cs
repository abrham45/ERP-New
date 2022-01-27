using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Qualification
    {
        public int Id { get; set; }
        public string Field { get; set; }
        public string Institution { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime EndYear { get; set; }
        public string QualificationType { get; set; }
 
      
    }
}
