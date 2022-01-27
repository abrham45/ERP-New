using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
      
        public string Name { get; set; }
   
        public string Summary { get; set; }
      
        public int DivisionId { get; set; }


        public Division Division { get; set; }
        public List<Team> Team { get; set; }

    }
}
