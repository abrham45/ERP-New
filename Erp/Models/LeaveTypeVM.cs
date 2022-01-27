using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class LeaveTypeVM
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default Number Of Days")]
        [Range(1, 200, ErrorMessage = "please Enter A valid Number")]
        public int DefaultDays { get; set; }
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
    }
}
