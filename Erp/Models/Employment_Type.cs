using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Employment_Type
    {
        [Key]
        public int Employment_TypeId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        public string Descrbtion { get; set; }

        public List<Employee> Employee { get; set; }
    }
}
