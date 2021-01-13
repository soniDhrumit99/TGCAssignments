using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccess.Interfaces;
using BusinessLogic.Interfaces;
using DatabaseModels.Models;
using static DatabaseModels.GlobalVariables;

namespace BusinessLogic.Classes
{
    public class BookingManager : IBookingManager
    {

        private readonly IBookingRepository _bookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public bool BookingExists(int id)
        {
            try
            {
                return _bookingRepository.DoesExist(id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Bookings DeleteBooking(int Id)
        {
            return PutBookingStatus(Id, "deleted");
        }

        public Bookings PutBookingDate(int Id, DateTime Date)
        {
            try
            {
                if (BookingExists(Id))
                {
                    Bookings booking = _bookingRepository.GetBooking(Id);
                    if (Date.Date < DateTime.UtcNow.Date)
                        throw new Exception("Invalid booking date");
                    else if (Date.Date == booking.BookingDate.Date)
                        throw new Exception("Invalid booking date");
                    else
                    {
                        return _bookingRepository.PutBookingDate(Id, Date.Date);
                    }
                }
                else
                {
                    throw new Exception("Invalid booking Id");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Bookings PutBookingStatus(int Id, string Status)
        {
            try
            {
                if (BookingExists(Id))
                {
                    switch (Status.ToLower())
                    {
                        case "optional":
                            throw new Exception("Booking status is already set to 'Optional'");
                        case "definitive":
                            return _bookingRepository.PutBookingStatus(Id, BookingStatus.Definitive);
                        case "cancelled":
                            return _bookingRepository.PutBookingStatus(Id, BookingStatus.Cancelled);
                        case "deleted":
                            return _bookingRepository.PutBookingStatus(Id, BookingStatus.Deleted);
                        default:
                            throw new Exception("Invalid booking status");
                    }
                }
                else
                {
                    throw new Exception("Invalid booking Id");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
