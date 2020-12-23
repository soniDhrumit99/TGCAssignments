using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SourceControlFinalAssignment.Models
{
    public class UserRegistrationViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Password is required to Login")]
        [DataType(DataType.Password, ErrorMessage = "Password does not match the required format")]
        [StringLength(16, ErrorMessage = "Maximum length of password is 16 characters only.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%&\/=?_.-]).{8,16}$", ErrorMessage = "Password does not match the required format")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [StringLength(30, ErrorMessage = "Name must be less than 30 characters long.")]
        [DataType(DataType.Text, ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [StringLength(80, ErrorMessage = "Email must be less than 80 characters long.")]
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Id")]
        public string Email { get; set; }

        [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Invalid Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Phone No.")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Birthdate is a required field.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Gender is a required field.")]
        [Range(0, 1, ErrorMessage = "Value for gender is out of range")]
        public short Gender { get; set; }

        [Required(ErrorMessage = "City is a required field.")]
        [StringLength(30, ErrorMessage = "City must be less than 30 characters long.")]
        [DataType(DataType.Text, ErrorMessage = "Invalid City Name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is a required field.")]
        [StringLength(30, ErrorMessage = "Country is a required field.")]
        [DataType(DataType.Text, ErrorMessage = "Invalid Country Name")]
        public string Country { get; set; }
    }
}