using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AssetRequest
    {
        public int Id { get; set; }
        public string Asset { get; set; }
        public int EmployeeId { get; set; }
        public string Description { get; set; }
        public DateTime RequestedDate { get; set; }
        public bool? Approved { get; set; }
        public User User { get; set; }
        public Employee Employee { get; set; }
        public List<AssetRequest> AssetRequests { get; set; }

    }
    public class AssetRequestViewVM
    {
        [Display(Name = "Total Number Of Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Rejected Requests")]
        public int RejectedRequests { get; set; }
        /*  public Employee Employee { get; set; }
 public List<AssetRequest> AssetRequests { get; set; }*/

    }
}
