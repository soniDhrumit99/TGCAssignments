using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModels.Models
{
    public class Bookings
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Booking date is a required field.")]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Room Id is a required field.")]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public GlobalVariables.BookingStatus Status { get; set; }
    }
}
