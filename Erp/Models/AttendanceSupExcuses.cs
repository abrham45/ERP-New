using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AttendanceSupExcuses
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AttendanceExcusesId { get; set; }
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public bool? Status { get; set; }
        public string Reason { get; set; }


        public AttendanceExcuses AttendanceExcuses { get; set; }
        public Attendances Attendances { get; set; }
        public Employee Employee { get; set; }

   
    }
}
