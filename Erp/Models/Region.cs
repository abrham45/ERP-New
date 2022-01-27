using Erp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Region
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RegionCode { get; set; }
        [Required]
        public string RegionName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Sex { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "invalid length")]
        public string Mobile { get; set; }

        [StringLength(13, MinimumLength = 10, ErrorMessage = "invalid length")]
        public string WorkTelephone { get; set; }

        public string AboutMe { get; set; }
        public string AreaOfExpertise { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
 

    }
}
