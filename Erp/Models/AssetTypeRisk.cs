using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AssetTypeRisk
    {
        internal string Asset_Type;

        public int Id { get; set; }
        public string Asset { get; set; }
        public int Asset_typeId { get; set; }
        public Asset_type Asset_types { get; set; }
        public decimal FailureProbability { get; set; }
        public decimal Loss { get; set; }
        public Risk Risk { get; set; }
        public decimal TotalAssetTypeRisk { get; set; }
    }
}
