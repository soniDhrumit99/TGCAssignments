using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModels.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Hotel Id is a required field.")]
        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        public Hotels Hotel { get; set; }

        [Required(ErrorMessage = "Room name is a required field.")]
        [StringLength(30, ErrorMessage = "Room name is either too long or too short.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Room category is a required field.")]
        public GlobalVariables.Categories Category { get; set; }

        [Required(ErrorMessage = "Room price is a  required field.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        public bool IsActive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
