using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Depersation
    {
        public int Id { get; set; }
        public int Salvage_Value { get; set; }

        public int Useful_Life_Time { get; set; }

        public int Annual_Depersation_Amount { get; set; }

        public int AssetId { get; set; }

        public Asset Asset { get; set; }

        public int PaymentId { get; set; }
    }
}

