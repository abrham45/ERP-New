using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Summary { get; set; }

    }
}
