using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Erp.Areas.Identity.Data;

namespace Erp.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string UserId { get; set; }
        public bool? status { get; set; }
        public DateTime sentDate { get; set; }
        public string reason { get; set; }
       // public DateTime Date { get; set; }
        public int  BasicSalaryId { get; set; }


        public Employee Employee { get; set; }
        public User User { get; set; }
    }
}
