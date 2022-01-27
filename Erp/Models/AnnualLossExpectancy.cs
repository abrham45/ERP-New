using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AnnualLossExpectancy
    {
        public int Id { get; set; }
        public int RiskId { get; set; } //forgine Key
        public decimal ExposureFactor { get; set; }
        public decimal SingleLossExpectancy { get; set; } //ExposureFactor * Value of Asset
        public decimal Value { get; set; } // Value of Asset
        public Decimal AnnualLossExpectancys { get; set; } // SingleLossExpectancy * Annual Rate of Occurance
        public Risk Risk { get; set; }
    }
}


