using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class MontlyReport
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FocusArea { get; set; }
        public string Month { get; set; }
        public string KeyMilestone { get; set; }
        public string Deliverables { get; set; }
        public string Challenge { get; set; }
        public string ChallengeOvercome { get; set; }
        public Employee Employee { get; set; }

    }
}
