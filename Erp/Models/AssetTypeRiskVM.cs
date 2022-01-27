using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AssetTypeRiskVM
    {
 
        public string AssetCatagory{ get; set; }
        public decimal TotalAssetTypeRisk { get; set; }


        List<AssetTypeRiskVM> AssetTypeRisk { get; set; }
    }
}
