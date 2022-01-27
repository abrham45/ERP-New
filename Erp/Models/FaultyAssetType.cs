using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class FaultyAssetType
    {
        public int Id { get; set; }
        public string FaultyType { get; set; }

        public string Reason { get; set; }
    }
}
