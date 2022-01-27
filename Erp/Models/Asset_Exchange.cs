using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Asset_Exchange
    {
        public int Id{ get; set; }
        public int from{ get; set; }
        public bool? status { get; set; }
        public int EmployeeId { get; set; }
        public int AssetId { get; set; }

        public Employee Employee { get; set; }
        public Asset Asset { get; set; }


    }
}
