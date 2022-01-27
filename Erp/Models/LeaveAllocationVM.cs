﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        //   public IEnumerable<SelectListItem> Employees { get; set; }
        //     public IEnumerable<SelectListItem> LeaveTypes { get; set; }
    }




    public class CreateLeaveAllocationVM
    {
        public int NumberUpdated { get; set; }
        public List<LeaveTypeVM> LeaveType { get; set; }
    }

    public class EditLeaveAllocationVM
    {
        public int Id { get; set; }

        public EmployeeVM Employee { get; set; }
        public int EmployeeId { get; set; }
        [Display(Name = "Number Of Days")]
        [Range(1, 50, ErrorMessage = "Enter Valid Number")]
        public int NumberOfDays { get; set; }
        public LeaveTypeVM LeaveType { get; set; }

    }
    public class ViewAllocationsVM
    {
        public EmployeeVM Employee { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Employeecode { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }

}

