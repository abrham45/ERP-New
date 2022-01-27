using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Risk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Priority { get; set; }
        public decimal AnnualOccurrence { get; set; }
        public decimal Likelyhood { get; set; }
        [DisplayFormat(DataFormatString = "{0:ddd, MMM d, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created_at { get; set; }
        public string Imapact { get; set; }
        public string Status { get; set; }
        public int RiskTypeId { get; set; }
        public RiskType RiskType { get; set; }

    }
}
