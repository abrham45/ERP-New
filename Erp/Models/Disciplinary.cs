
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Disciplinary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DisciplinaryTypeId { get; set; }
        public string Description { get; set; }
        public bool?  Approved { get; set; }
        public DateTime Date { get; set; }
        public string  Detention { get; set; }
    }
}
