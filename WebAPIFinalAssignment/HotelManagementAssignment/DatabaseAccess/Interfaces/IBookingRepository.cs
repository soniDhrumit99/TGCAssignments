using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.Models;

namespace DatabaseAccess.Interfaces
{
    public interface IBookingRepository
    {
        Bookings PostBooking(Bookings booking);
        Bookings GetBooking(int Id);
        Bookings PutBookingDate(int Id, DateTime Date);
        Bookings PutBookingStatus(int Id, DatabaseModels.GlobalVariables.BookingStatus Status);
        Bookings DeleteBooking(int Id);
        bool DoesExist(int Id);
    }
}
