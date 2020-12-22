using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SourceControlFInalAssignment.Models
{
    public class LoginModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is a required field.")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is a required field.")]
        public string Password { get; set; }
    }
}