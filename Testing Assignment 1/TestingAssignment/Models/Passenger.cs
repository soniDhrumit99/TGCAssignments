using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestingAssignment.Models
{
    public class Passenger
    {
        [Key]
        public int Id { set; get; }

        [Required(ErrorMessage = "Firstname is a required field")]
        [DataType(DataType.Text, ErrorMessage = "Firstname must be a string")]
        [StringLength(30, ErrorMessage = "Firstname is too long")]
        public string firstname { set; get; }

        [Required(ErrorMessage = "Lastname is a required field")]
        [DataType(DataType.Text, ErrorMessage = "Lastname must be a string")]
        [StringLength(30, ErrorMessage = "Lastname is too long")]
        public string lastname { set; get; }

        [Required(ErrorMessage = "Firstname is a required field")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        public string phone { set; get; }

        public Passenger(int id, string fname, string lname, string phone)
        {
            this.Id = id;
            this.firstname = fname;
            this.lastname = lname;
            this.phone = phone;
        }

        public Passenger()
        {

        }
    }
}