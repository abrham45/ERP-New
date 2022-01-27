using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public int Duration { get; set; }
        public string Description { get; set; }
        [Required]
        public Decimal TaskCost { get; set; }
        [Range(typeof(int), "1", "100")]
        public int TaskProgress { get; set; }
        public Decimal PercentCoverage { get; set; }
        public bool? Status { get; set; }
        public int ProjectId { get; set; }
        public int? AssignId { get; set; }
        

        public Project Project { get; set; }
        public Assign Assign { get; set; }



    }
}