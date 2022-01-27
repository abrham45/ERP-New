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
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly EmployeeContext _context;

        public LeaveRequestRepository(EmployeeContext context)
        {
            _context = context;
        }

        public bool Create(LeaveRequest entitiy)
        {
            _context.LeaveRequests.Add(entitiy);
            return Save();
        }

        public bool Delete(LeaveRequest entitiy)
        {
            _context.LeaveRequests.Remove(entitiy);
            return Save();
        }

        public ICollection<LeaveRequest> FindAll()
        {
            var LeaveRequest = _context.LeaveRequests
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .OrderByDescending(q=>q.Id)
                /*.Include(a=>a.)*/
                .ToList();
            return LeaveRequest;
        }

      /*  public LeaveRequest FindById(int id)
        {
            throw new NotImplementedException();
        }
*/
        public LeaveRequest FindById(int id)
        {

            var leaveRequest = _context.LeaveRequests.Find(id);
            
     
            return leaveRequest;

          
        }

        public bool isExists(int id)
        {
            var exists = _context.LeaveRequests.Any(q => q.Id == id);

            return exists;
        }

        public bool Save()
        {
            var changes = _context.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveRequest entitiy)
        {
            _context.LeaveRequests.Update(entitiy);
            return Save();
        }



        public List<LeaveRequest> GetLeaveRequestsFromId(int empid)
        {
            return _context.LeaveRequests.Where(q => q.EmployeeId == empid).ToList();

        }


    }
}
