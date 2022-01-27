using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class OrderAsset
    {
        [Key]
        public int Id { get;    set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssetTypeId { get; set; }
        [Required]
        public int Quantity  { get; set; }
        [Required]
        public Decimal EstimatedPrice  { get; set; }
        public int EmployeeId  { get; set; }
        public bool?  Status { get; set; }
        public string Specification  { get; set; }
        public DateTime OrderedDate  { get; set; }

        public Employee Employee { get; set; }
        public Asset_type Asset_type { get; set; }
    }
}
