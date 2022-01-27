using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class FieldWork
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public int Duration { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; } 
        public  decimal? PerDay { get; set; }
        public int? EmployeeId { get; set; }
        public int? ProjectId { get; set; }
        public int? DriverId { get; set; }
        public bool? Status { get; set; }
        public Driver Driver { get; set; }
        public Employee Employee { get; set; }
        public Project Project { get; set; }

    }
}
