using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Division
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string OfficeNumber { get; set; }


        public List<Department> Department { get; set; }

    }
}
