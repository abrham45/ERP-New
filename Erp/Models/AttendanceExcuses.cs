using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AttendanceExcuses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ExcuseName { get; set; }
        public string Description { get; set; }
       // public string Cause { get; set; }
    }
}
