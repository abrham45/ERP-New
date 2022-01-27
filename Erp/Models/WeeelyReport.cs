using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class WeeelyReport
    {
        public int Id{ get; set; }
        public string WeeklyRecap { get; set; }
        public string TaskRecap { get; set; }
        public string TaskUnfinshed { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Challenge { get; set; } 
        public  bool? status { get; set; }
        public int EmployeeId { get; set; }
        public int TeamId { get; set; }
        public string ChallengeOvercome { get; set; }

        public Employee Employees { get; set; }
        public Team Team { get; set; }


    }
}
