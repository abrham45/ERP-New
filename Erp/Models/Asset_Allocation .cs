using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Asset_Allocation
    {
        public int Id{ get; set; }

        public int EmployeeId { get; set; }

        public int AssetId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }

        public Asset Asset { get; set; }

        public Employee Employee { get; set; }


    }
}
