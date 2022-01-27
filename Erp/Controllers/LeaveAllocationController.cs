using AutoMapper;
using Erp.Areas.Identity.Data;
using Erp.Contracts;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveTypeRepository _leaverepo;
        private readonly ILeaveAllocationRepository _leaveallocationrepo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly EmployeeContext _context;

        public LeaveAllocationController(ILeaveTypeRepository leaverepo, ILeaveAllocationRepository leaveallocationrepo, IMapper mapper, UserManager<User> userManager, EmployeeContext context)
        {
            _leaverepo = leaverepo;
            _leaveallocationrepo = leaveallocationrepo;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        // GET: LeaveAllocationController
        public ActionResult Index()
        {
            var leaveallocation = _leaverepo.FindAll().ToList();
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveallocation);
            var model = new CreateLeaveAllocationVM
            {
                LeaveType = mappedLeaveTypes,
                NumberUpdated = 0
            };
            return View(model);
        }

        public ActionResult SetLeave(int id)
        {

            var leavetype = _leaverepo.FindById(id);
            //var employees = _userManager.GetUsersInRoleAsync("Basic").Result;
            var employees = _context.Employees;
            foreach (var item in employees)
            {
                if (_leaveallocationrepo.CheckAllocation(id, item.EmployeeCode))
                    continue;

                var allocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = item.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leavetype.DefaultDays,
                    Period = DateTime.Now.Year,
                };


                var leaveallocation = _mapper.Map<LeaveAllocation>(allocation);
                _leaveallocationrepo.Create(leaveallocation);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {

            var employees = _userManager.GetUsersInRoleAsync("Basic");
            var model = _mapper.Map<List<EmployeeVM>>(employees);
            return View(model);

        }

        // GET: LeaveAllocationController/Details/5
        public ActionResult Details(int id)
        {
            //var employee = _mapper.Map<EmployeeVM>( _context.LeaveAllocation.FirstOrDefault(q=>q.EmployeeId==id.ToString()));

            // var employee = _context.Users.FirstOrDefault(m => m.Id == id);

            var employee = _context.Employees.FirstOrDefault(m => m.Id == id);
            var allocations = _mapper.Map<List<LeaveAllocationVM>>(_leaveallocationrepo.GetLeaveAllocationsByEmployee(id));
            var model = new ViewAllocationsVM
            {
                EmployeeId = employee.Id,
                FirstName = employee.FirstName,
                //Employee= employee.FirstName,
                //                Email = employee.Email,
                //LastName = employee.user.LastName,
                Employeecode = employee.EmployeeCode,
                LeaveAllocations = allocations
            };
            return View(model);
        }

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            var leaveallocation = _leaveallocationrepo.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationVM>(leaveallocation);
            return View(model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var record = _leaveallocationrepo.FindById(model.Id);
                record.NumberOfDays = model.NumberOfDays;
                //var allocation = _mapper.Map<LeaveAllocation>(model);
                var isSuccess = _leaveallocationrepo.Update(record);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error while saving");
                    return View(model);
                }
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });

            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
