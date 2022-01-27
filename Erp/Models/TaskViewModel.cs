using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class TaskViewModel
    {
        public int pending { get; set; }
        public int Completed { get; set; }
        public int NotStarted { get; set; }

    }
}
