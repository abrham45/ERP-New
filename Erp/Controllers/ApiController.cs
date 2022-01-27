using Erp.Areas.Identity.Data;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    [ApiController]
    [Route("api/[Action]")]
    public class ApiController : ControllerBase
    {
        
        private readonly EmployeeContext _context;
        private readonly UserManager<User> _userManager;

        public ApiController(EmployeeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public TaskViewModel GetTasks()
        {
            var users = _userManager.GetUserId(User);
            var emp = _context.Employees.FirstOrDefault(e => e.UserId == users);

            var completed = _context.Taskss.Include(e => e.Project).Include(e=>e.Project.Employee).Where(e => e.Status == true & e.Project.EmployeeId == emp.Id).ToList().Count;
            var notStarted = _context.Taskss.Include(e => e.Project).Include(e => e.Project.Employee).Where(e => e.Status == null & e.Project.EmployeeId == emp.Id).ToList().Count;
            var pending = _context.Taskss.Include(e => e.Project).Include(e => e.Project.Employee).Where(e => e.Status == false & e.Project.EmployeeId == emp.Id).ToList().Count;


            TaskViewModel alljson = new TaskViewModel();
            alljson.Completed = completed;
            alljson.NotStarted = notStarted;
            alljson.pending = pending;

            return alljson;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Tasks> GetTaskDue()
        {

            var Tasks = _context.Taskss.ToList();

            return Tasks;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public RiskViewModel GetTaskHigh()
        {

            var Low = _context.Risk.Where(e => e.Status == "Low").ToList().Count;
            var Medium = _context.Risk.Where(e => e.Status == "Medium").ToList().Count;
            var High = _context.Risk.Where(e => e.Status == "High").ToList().Count;

            RiskViewModel Risk = new RiskViewModel();
            Risk.Low = Low;
            Risk.High = High;
            Risk.Medium = Medium;

            return Risk;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public List<AssetTypeRisk> GetTaskRisk()
        {

            var Asset = _context.AssetTypeRisk.Include(e => e.Asset_types)
                .Select(e => new AssetTypeRisk
                { Asset = e.Asset_types.Asset_Type, TotalAssetTypeRisk = e.TotalAssetTypeRisk })
                .ToList();

            return Asset;
        }
        ////////
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        ////////
    }
}
