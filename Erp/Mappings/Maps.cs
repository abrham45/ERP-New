using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.Areas.Identity.Data;
using AutoMapper;
using Erp.Models;

namespace Erp.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            AllowNullDestinationValues = true;
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, EditLeaveAllocationVM>().ReverseMap();
          

            //CreateMap<IEnumerable<EmployeeVM>>(Employee);


        }

    }
}

