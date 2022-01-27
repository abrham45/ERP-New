using Erp.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Erp.Areas.Identity.Pages.Account.ExternalLoginModel;

namespace Erp.Models
{
    public class Employee
    {

        [Key]
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(13,MinimumLength =10,ErrorMessage = "invalid length")]
        public string Mobile { get; set; }

        [StringLength(13, MinimumLength = 10, ErrorMessage = "invalid length")]
        public string HomeTelephone { get; set; }

        [StringLength(13, MinimumLength = 10, ErrorMessage = "invalid length")]
        public string WorkTelephone { get; set; }
        public int? Fax { get; set; }
        public int? POBox { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        public string Subcity { get; set; }
        [Required]
        public string Woreda { get; set; }
        public bool Status { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string AboutMe { get; set; }
        public string AreaOfExpertise { get; set; }
        [Required]
         
        public int AccountNumber { get; set; }
        public bool? Approve { get; set; }
        public Boolean Isin { get; set; } /*For Attendance*/

        public int DivisionId { get; set; }

        public int DepartmentId { get; set; }
        public int TeamId { get; set; }
        public int Employment_typeId { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        /*   public int QualificationId { get; set; }
           public int SocialsId { get; set; }
           public int EmergencyContactId { get; set; }*/

        public string Field { get; set; }
        public string Institution { get; set; }
        public DateTime QstartYear { get; set; }
        public DateTime QendYear { get; set; }
        public string QualificationType { get; set; }

        public string sName { get; set; }
        public string sUrl { get; set; }

        public string EName { get; set; }
        public string EAddress { get; set; }
        public string EPhoneNumber { get; set; }
        public string ERelation { get; set; }

        public Department Department { get; set; }
        public Employment_Type Employment_Type { get; set; }
        public Position Positions { get; set; }
        public Division Division { get; set; }
        public Team Team { get; set; }
        public List<Qualification> Qualification { get; set; }
        public  User User { get; set; }
        public Socials Socials { get; set; }

        public EmergencyContact EmergencyContact { get; set; }
    /*    public List<Attendance> Attendances { get; set; }*/

        public List<Attendances> Attendancess { get; set; }

   
    }

}