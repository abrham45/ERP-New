using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        [DisplayName("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [Required()]
        public Employee Employee { get; set; }
        public User User { get; set; }

        [Required()]
        [DisplayFormat(DataFormatString = "{0:ddd, MMM d, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required()]
        public TimeSpan TimeIn { get; set; }
        [AllowNull]
        public TimeSpan TimeOut { get; set; }


    }
}
