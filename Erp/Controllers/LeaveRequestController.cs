
using AutoMapper;
using Erp.Areas.Identity.Data;
using Erp.Contracts;
using Erp.Data;
using Erp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    public class LeaveRequestController : Controller
    {

        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly ILeaveTypeRepository _leaveType;
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveallocationrepo;
        private readonly UserManager<User> _userManager;


        private readonly EmployeeContext _context;

        public LeaveRequestController(ILeaveRequestRepository leaveRequestRepo, IMapper mapper, EmployeeContext context, ILeaveTypeRepository leaveType, ILeaveAllocationRepository leaveallocationrepo, UserManager<User> userManager)
        {

            _leaveRequestRepo = leaveRequestRepo;
            _mapper = mapper;
            _context = context;
            _leaveType = leaveType;
            _leaveallocationrepo = leaveallocationrepo;
            _userManager = userManager;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: LeaveRequestController

        public async Task<IActionResult> Index()
        {

            User user = await _userManager.GetUserAsync(User);
            var Dc = _context.Employees.FirstOrDefault(a => a.UserId == user.Id);

            if (Dc != null)
            {

                var leaveRequests = _context.LeaveRequests
                                        .Where(e => e.EmployeeId == Dc.Id)
                                        .Include(e=> e.Employee).Include(e=>e.LeaveType)
                                        .OrderByDescending(e=>e.Id);

                var leaveRequestsModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);
                var model = new AdminLeaveRequestViewVM
                {
                    TotalRequests = leaveRequestsModel.Count,
                    ApprovedRequests = leaveRequestsModel.Count(q => q.Approved == true),
                    PendingRequests = leaveRequestsModel.Count(q => q.Approved == null),
                    RejectedRequests = leaveRequestsModel.Count(q => q.Approved == false),
                    LeaveRequests = leaveRequestsModel,
                };

                return View(model);
                /**//*return View(await empall.ToListAsync());*/
            }
            else
            {
                return NotFound();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Director")]
        public IActionResult IndexDir()
        {

            var leaveRequests = _leaveRequestRepo.FindAll();
            var leaveRequestsModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);

            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequestsModel.Count,
                ApprovedRequests = leaveRequestsModel.Count(q => q.Approved == true),
                PendingRequests = leaveRequestsModel.Count(q => q.Approved == null),
                RejectedRequests = leaveRequestsModel.Count(q => q.Approved == false),
                LeaveRequests = leaveRequestsModel,
            };

            return View(model);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
      
        public ActionResult Details(int id)
        {
            var leaveRequest = _leaveRequestRepo
                .FindById(id);
          //var gg=_context.LeaveRequests.Include(a=>a.Employee).FirstOrDefaultAsync(m => m.Id == id);
            var model = _mapper.Map<LeaveRequestVM>(leaveRequest);

        

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult ApproveRequest(int id)
        {

            var user = _userManager.GetUserAsync(User).Result;
            var leaveRequest = _leaveRequestRepo.FindById(id);
            var leav = _context.LeaveRequests.FirstOrDefault(a=>a.Id == id);
          /*  leav.Feedback = Convert.ToString(HttpContext.Request.Form["FeedbackId"]);
            _context.Add(leav.Feedback);
            _context.SaveChanges();*/
            leaveRequest.Approved = true;
            leaveRequest.Feedback = Convert.ToString(HttpContext.Request.Form["Feedback"]);
            leaveRequest.ApprovedById = id;
            leaveRequest.DateActioned = DateTime.Now;
           /* leaveRequest.Feedback = 
*/
            var isSuccess = _leaveRequestRepo.Update(leaveRequest);
            return RedirectToAction(nameof(IndexDir), "LeaveRequest");


        }

        public ActionResult RejectRequest(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var leaveRequest = _leaveRequestRepo.FindById(id);
            leaveRequest.Approved = false;
           /* leaveRequest.Feedback = Convert.ToString(HttpContext.Request.Form["Feedback"]);*/
            //     leaveRequest.ApprovedById = user.EmployeeId;
            leaveRequest.DateActioned = DateTime.Now;

            var isSuccess = _leaveRequestRepo.Update(leaveRequest);
             return RedirectToAction(nameof(IndexDir), "LeaveRequest");



        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public ActionResult Create()
        {

            var leaveTypes = _leaveType.FindAll();

            var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            var model = new CreateLeaveRequestVM
            {
                
                LeaveTypes = leaveTypeItems
            };
            return View(model);

        }


        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(CreateLeaveRequestVM model)
        {
            try
            {
                var Rs = Convert.ToString(model.Reason);
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                //  var leaveTypes = _leaveType.FindAll();
                User user = await _userManager.GetUserAsync(User);
                var emp = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);
                // var allocation = _leaveallocationrepo.GetLeaveAllocationsByEmployeeType(user.Id,model.LeaveTypeId);
                var period = DateTime.Now.Year;
                int daysRequested = (int)(endDate - startDate).TotalDays;
                var leaveTypes = _leaveType.FindAll();

                var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });
                model.LeaveTypes = leaveTypeItems;

                /*
                if (daysRequested> allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You dont have sufficient dyas for this request");
                    return View(model);
                }
                */
                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    ModelState.AddModelError("", "Start Date cannot be further in the future than the End Date");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestVM
                {
                    Reason = Rs,
                    StartDate = startDate,
                    EndDate = endDate,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId,
                    EmployeeId = emp.Id
                };

                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
                _leaveRequestRepo.Create(leaveRequest);
                _leaveRequestRepo.Save();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }
        // GET: LeaveRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LeaveRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Delete/5
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
