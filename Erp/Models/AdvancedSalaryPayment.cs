using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AdvancedSalaryPayment
    {
        [Key]

        public int Id { get; set; }
        public DateTime Month { get; set; }
        public decimal PerMonthAmount { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime DeductionDate { get; set; }
        public int AdvancedSchemeId{ get; set; }   
        public int EmplpyeeId { get; set; }


        public Employee Employee { get; set; }
        public AdvancedScheme AdvancedScheme { get; set; }
        //to check

        //  Should we add advanced scheme relation attribute


    }
}
