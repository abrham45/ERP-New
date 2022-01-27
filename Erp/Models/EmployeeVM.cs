using Erp.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class EmployeeVM
    {

        public int Id { get; set; }
        [Required]

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
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string HomeTelephone { get; set; }
        public string WorkTelephone { get; set; }
        public int Fax { get; set; }
        public int POBox { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }

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




      /*  public int DepartmentId { get; set; }
        public int Employment_typeId { get; set; }
        public int PositionId { get; set; }
        public Boolean Isin { get; set; }
        public int AuthorityId { get; set; }
        public string UserId { get; set; }
        public int EmergencyContactId { get; set; }
        public int QualificationId { get; set; }
        public string SocialsId { get; set; }*/



        public IEnumerable<SelectListItem> QulificationType { get; set; }
        [Display(Name = "Qualification Type")]
     /*   public int EmployeeId { get; set; }*/

        public Employee Employee { get; set; }
        public List<EmployeeVM> EmployeeVMs { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> Employment_Types { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }
        public IEnumerable<SelectListItem> Authoritys { get; set; }
        public User User { get; set; }
        public Socials Socials { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
        public List<Attendances> Attendances { get; set; }
     



    }

}