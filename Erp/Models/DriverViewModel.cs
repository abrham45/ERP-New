using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class DriverViewModel
    {

        public string DriverId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int ExperienceInYears { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
        public string Date_of_birth { get; set; }
        [Required]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "invalid length")]
        public string Mobile { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        public string Subcity { get; set; }
        [Required]
        public string Woreda { get; set; }
        public bool? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LicenseNumber { get; set; }

        public IFormFile LicenseImage { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
