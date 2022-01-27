using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class FaultyAsset
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public int AssetId { get; set; }

        public int FaultyAssetTypeId { get; set; }
        public bool? status { get; set; }

        public User User { get; set; }

       public Asset Asset { get; set; }

        public FaultyAssetType FaultyAssetType { get; set; }

    }
}
