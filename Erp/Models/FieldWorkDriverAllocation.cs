using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class FieldWorkDriverAllocation
    {
        public int Id { get; set; }
        public int FieldWorkId { get; set; }

        [DisplayFormat(DataFormatString = "{mm:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDay { get; set; }
        public int? DriverId { get; set; }
        public int EmployeeId { get; set; }
        public decimal PerDay  { get; set; }
        public Driver Driver { get; set; }
        public Employee Employee { get; set; }
        public FieldWork FieldWork { get; set; }
    }
}
