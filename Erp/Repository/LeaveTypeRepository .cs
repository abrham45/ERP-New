using Erp.Areas.Identity.Data;
using Erp.Contracts;
using Erp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class LeaveTypeRepository:  ILeaveTypeRepository 
    {
        private readonly EmployeeContext _context;

    public LeaveTypeRepository(EmployeeContext context)
    {
        _context = context;
    }
    public bool Create(LeaveType entitiy)
    {
        _context.LeaveType.Add(entitiy);
        return Save();
    }

    public bool Delete(LeaveType entitiy)
    {
        _context.LeaveType.Remove(entitiy);
        return Save();
    }

    public ICollection<LeaveType> FindAll()
    {
        var leavetypes = _context.LeaveType.ToList();
        return leavetypes;
    }

    public LeaveType FindById(int id)
    {
        var leavetype = _context.LeaveType.Find(id);
        return leavetype;

    }

    public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
    {
        throw new NotImplementedException();
    }

    public bool isExists(int id)
    {
        var exists = _context.LeaveType.Any(q => q.Id == id);

        return exists;
    }

    public bool Save()
    {
        var changes = _context.SaveChanges();
        return changes > 0;
    }

    public bool Update(LeaveType entitiy)
    {
        _context.LeaveType.Update(entitiy);
        return Save();
    }
}
}
