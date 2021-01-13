using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.Models;

namespace BusinessLogic.Interfaces
{
    public interface IBookingManager
    {
        Bookings PutBookingDate(int Id, DateTime Date);
        Bookings PutBookingStatus(int Id, string Status);
        Bookings DeleteBooking(int Id);
        bool BookingExists(int id);
    }
}
