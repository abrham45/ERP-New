using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class RequestVehicle
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public int? RouteId { get; set; }
        public string Destination { get; set; }
        public string? Feedback { get; set; }
        public Route Route { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employees { get; set; }
    }
}
