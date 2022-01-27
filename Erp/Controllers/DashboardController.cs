using Erp.Areas.Identity.Data;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    public class DashboardController : Controller
    {


        private readonly EmployeeContext _context;

        public DashboardController(EmployeeContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var emps = _context.Employees.ToList();
            var deps = _context.Departments.ToList();
            var LeaveRequest = _context.LeaveRequests
               .Include(q => q.LeaveType)
               .Include(q => q.Employee)
               .ToList();

            var model = new Dashboard
            {
                TotalRequests = LeaveRequest.Count,
                TotalDepartments = deps.Count,
                TotalEmployees = emps.Count
            };

            return View(model);

          
        }

     
        
    }
}
