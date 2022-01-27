using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class SendFile
    {
        [Key]
        public int Id { get; set; }
        public string path { get; set; }
        public int senderId { get; set; }
        public int ReciverId { get; set; }
        public DateTime DateTime { get; set; }

        public bool? filesatuts { get; set; }
        public Employee Employee { get; set; }
    }
}
