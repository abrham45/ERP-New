using Erp.Areas.Identity.Data;
using Erp.Contracts;
using Erp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {

        private readonly EmployeeContext _context;

        public LeaveAllocationRepository(EmployeeContext context)
        {
            _context = context;
        }

        public bool CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(q => q.EmployeeId == employeeid && q.LeaveTypeId == leavetypeid && q.Period == period).Any();
        }

        public bool Create(LeaveAllocation entitiy)
        {
            _context.LeaveAllocation.Add(entitiy);
            return Save();
        }

        public bool Delete(LeaveAllocation entitiy)
        {
            _context.LeaveAllocation.Remove(entitiy);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            var leaveAllocation = _context.LeaveAllocation
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .ToList();
            return leaveAllocation;
        }

        public LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _context.LeaveAllocation
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefault(q => q.Id == id);
            return leaveAllocation;

        }


        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(int id)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                .Where(q => q.Id == id && q.Period == period)
                .ToList();
        }

        public LeaveAllocation GetLeaveAllocationsByEmployeeType(string id, int leavetypeid)
        {
            var period = DateTime.Now.Year;
            var allocations = FindAll();
            return
                allocations.FirstOrDefault(q => q.EmployeeId == id && q.Period == period && q.LeaveTypeId == leavetypeid);
        }

        public LeaveAllocation GetLeaveAllocationsByEmployeeType(int id, int leavetypeid)
        {
            throw new NotImplementedException();
        }

        public bool isExists(int id)
        {
            var exists = _context.LeaveAllocation.Any(q => q.Id == id);

            return exists;
        }

        public bool Save()
        {
            var changes = _context.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveAllocation entitiy)
        {
            _context.LeaveAllocation.Update(entitiy);
            return Save();
        }
    }
}
