using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatabaseModels.Models
{
    public class Hotels
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Hotel Name is a required field.")]
        [StringLength(30, ErrorMessage = "Hotel name is either too long or too short.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hotel Address is a required field.")]
        [StringLength(200, ErrorMessage = "Address is either too long or too short.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is a required field.")]
        [StringLength(20, ErrorMessage = "City Name is either too long or too short.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pincode is a required field.")]
        public int Pincode { get; set; }

        [Required(ErrorMessage = "Contact number is a required field.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Contact Number.")]
        public long ContactNumber { get; set; }

        [Required(ErrorMessage = "Contact person is a required field.")]
        [StringLength(30, ErrorMessage = "Contact person name is either too long or too short.")]
        public string ContactPerson { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }

        [DataType(DataType.Url)]
        public string Facebook { get; set; }

        [DataType(DataType.Url)]
        public string Twitter { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
