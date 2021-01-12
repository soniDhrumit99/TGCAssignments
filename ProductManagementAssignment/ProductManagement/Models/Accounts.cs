using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class Accounts
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Username is a required field.")]
        [DataType(DataType.Text, ErrorMessage = "Username can only be Text")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is a required field.")]
        [DataType(DataType.Password, ErrorMessage = "Please enter a valid password")]
        public string Password { get; set; }
    }
}