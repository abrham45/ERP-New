using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class maintainance
    {
        public int Id { get; set; }
        public DateTime InitiatedDate { get; set; }
        public string Reportedto { get; set; }
        public int departmentId { get; set; }
        public DateTime ReqiredDate { get; set; }

        public int empID { get; set; } // for 

        public bool? stauts { get; set; }

    }
}
