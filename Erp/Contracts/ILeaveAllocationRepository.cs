using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Contracts
{

    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool CheckAllocation(int leavetypeid, string employeeid);
        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(int id);
        LeaveAllocation GetLeaveAllocationsByEmployeeType(int id, int leavetypeid);

    }
}
