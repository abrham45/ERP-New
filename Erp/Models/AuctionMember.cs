using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class AuctionMember
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AuctionNumber { get; set; }
        [Required]
        public string TinNumber { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Specification { get; set; }
        public Decimal Price { get; set; }
        public bool? Status { get; set; }
        public DateTime Date { get; set; }
        public string Feedback { get; set; }
    }
}
