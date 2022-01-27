using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    [Authorize]
    public class DashboardPm : Controller
    {
        private readonly ILogger<DashboardPm> _logger;
        private readonly EmployeeContext _context;
        public DashboardPm(ILogger<DashboardPm> logger, EmployeeContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// //
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var noti = _context.Taskss.ToList();
            return View(noti);
        }
        /// <summary>
        /// //
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> IndexRisk()
        {
            var noti = _context.Risk.ToList();
            return View(noti);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}