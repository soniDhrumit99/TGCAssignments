using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SourceControlFInalAssignment.CustomAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceControlFInalAssignment.Models
{
    public class UserModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Username")]
        [StringLength(50, ErrorMessage = "Name length must be less than 50 characters")]
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is a required field")]
        [Display(Name = "Email id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is a required field.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Profile photo is required")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg, jpeg, png")]
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Birthdate")]
        [AgeValidation()]
        public string BirthDate { get; set; }
    }
}