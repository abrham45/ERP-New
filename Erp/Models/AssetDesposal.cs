using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AssetDesposal
    {
        public int Id { get; set; }
        // public int FaultyAssetId { get; set; }
        public int AssetId { get; set; }
        public string Reason { get; set; }
        public DateTime TransferDate { get; set; }
        public Asset Asset { get; set; }
        //  public FaultyAsset FaultyAsset { get; set; }
    }
}
