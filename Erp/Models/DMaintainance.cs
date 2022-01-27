using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class DMaintainance
    {
        [Key]
        public int Id { get; set; }
        public int vechiclesId { get; set; }
        public bool? Status { get; set; }
        public string? Feedback { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int DriverId { get; set; }

        public Driver Driver { get; set; }
        public vechicles vechicles { get; set; }
    }
}
