using Erp.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string DriverId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int ExperienceInYears { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
        public DateTime Date_of_birth { get; set; }
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
        public string UserId { get; set; }
        public string LicensePictureName { get; set; }
        public string ProfilePictureName { get;  set; }
        public byte[] LicensePicture { get; set; }
        public byte[] ProfilePicture { get; set; }

        public User User { get; set; }
    }
}
