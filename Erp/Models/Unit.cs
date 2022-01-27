using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }

        public int DepartmentId { get; set; }
        /*        [ForeignKey("DepartmentId")]*/


        public int PostionId { get; set; }
        /* [ForeignKey("PostionId")]*/


        public string Description { get; set; }

    }
}
