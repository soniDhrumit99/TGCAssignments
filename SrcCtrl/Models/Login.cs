using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SourceControlFinalAssignment.Models
{
    public class Login
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required to Login")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Username or Email Id")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required to Login")]
        [DataType(DataType.Password, ErrorMessage = "Password does not match the required format")]
        [StringLength(16, ErrorMessage = "Maximum length of password is 16 characters only.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%&\/=?_.-]).{8,16}$", ErrorMessage = "Password does not match the required format")]
        public string Password { get; set; }
    }
}