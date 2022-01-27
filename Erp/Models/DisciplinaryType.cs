using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class DisciplinaryType
    {
        [Key]
        public int id { get; set; }
        public  string type { get; set; }

        public string Describtion { get; set; }
    }
}
