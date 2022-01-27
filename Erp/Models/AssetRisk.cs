using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AssetRisk
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public Asset Assets { get; set; }
        public Risk Risk { get; set; }
        public  decimal Loss { get; set; }
        public decimal FailureProbability { get; set; }
        public int RiskId { get; set; }
        public decimal AssetRisks { get; set; }

    }
}
