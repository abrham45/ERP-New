using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public Decimal ProjectBudget { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public int ProjectDuration { get; set; }
        public int EmployeeId  { get; set; }
        public int DirectorId { get; set; }
        public bool? Status { get; set; }

        public Employee Employee { get; set; }

       
    }
}
