using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccess.Interfaces;
using DatabaseAccess.Context;
using static DatabaseModels.GlobalVariables;
using DatabaseModels.Models;

namespace DatabaseAccess.Classes
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext db;

        public BookingRepository()
        {
            db = new AppDbContext();
        }

        public Bookings PostBooking(Bookings booking)
        {
            try
            {
                booking = db.Bookings.Add(booking);
                db.SaveChanges();
                return booking;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Bookings GetBooking(int Id)
        {
            try
            {
                var result = db.Bookings.Find(Id);
                if (result != null)
                    return result;
                else
                    throw new Exception("Invalid Booking Id");
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public Bookings PutBookingDate(int Id, DateTime Date)
        {
            try
            {
                Bookings booking = db.Bookings.Find(Id);
                if (booking != null)
                {
                    booking.BookingDate = Date.Date;
                    db.SaveChanges();
                    return booking;
                }
                else
                {
                    throw new Exception("Invalid Booking Id");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Bookings PutBookingStatus(int Id, BookingStatus Status)
        {
            try
            {
                Bookings booking = db.Bookings.Find(Id);
                if (booking != null)
                {
                    booking.Status = Status;
                    db.SaveChanges();
                    return booking;
                }
                else
                {
                    throw new Exception("Invalid Booking Id");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Bookings DeleteBooking(int Id)
        {
            throw new NotImplementedException();
        }

        public bool DoesExist(int Id)
        {
            try
            {
                var result = db.Bookings.Find(Id);
                bool exists;
                if (result != null && result.Status != BookingStatus.Deleted)
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }
                return exists;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
