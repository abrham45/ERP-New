using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AuctionOwner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AuctionNumber { get; set; }
        public string  AssetName{ get; set; }
        public string Description  { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Specification { get; set; }
        public DateTime Date  { get; set; }

    }
}
