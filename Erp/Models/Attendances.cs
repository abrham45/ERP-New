using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Attendances
    {
        public static bool IsModified { get; internal set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
      
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public TimeSpan MorningCheckin { get; set; }
        public TimeSpan MorningCheckout { get; set; }    
        public TimeSpan AfternoonCheckin { get; set; }
        public TimeSpan AfternoonCheckout { get; set; }
        public string Status { get; set; }
        public TimeSpan MorningWorkingHour { get; set; }

        public TimeSpan AfternoonWorkingHour { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan WorkHour { get; set; }

        public Employee Employee { get; set; }

        public LateCome LateCome { get; set; }
        public LeaveRequest LeaveRequest { get; set; }


    }
}
