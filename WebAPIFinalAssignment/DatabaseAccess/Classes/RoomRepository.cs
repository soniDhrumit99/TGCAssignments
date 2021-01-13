using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccess.Interfaces;
using DatabaseModels.Models;
using DatabaseAccess.Context;
using System.Web;
using System.Net;
using static DatabaseModels.GlobalVariables;

namespace DatabaseAccess.Classes
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext db;

        public RoomRepository()
        {
            db = new AppDbContext();
        }

        public string DeleteRoom(int Id)
        {
            throw new NotImplementedException();
        }

        public bool GetAvailability(int Id, DateTime Date)
        {
            try
            {
                Bookings booking = db.Bookings.Where(x => x.RoomId == Id && x.BookingDate == Date && x.Status != BookingStatus.Deleted).FirstOrDefault();
                bool available;
                if(booking == null)
                {
                    available = true;
                }
                else
                {
                    available = false;
                }
                return available;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Room> GetRooms(int? Pincode, decimal? Price, Categories? Category, string HotelCity = null)
        {
            IQueryable<Room> rooms = db.Rooms.Where(x => x.IsActive == true);

            switch (Category)
            {
                case Categories.Small:
                    rooms = rooms.Where(x => x.Category == Categories.Small);
                    break;
                case Categories.Medium:
                    rooms = rooms.Where(x => x.Category == Categories.Medium);
                    break;
                case Categories.Large:
                    rooms = rooms.Where(x => x.Category == Categories.Large);
                    break;
                default:
                    break;
            }

            if(Pincode != null)
            {
                rooms = rooms.Where(x => x.Hotel.Pincode == Pincode);
            }
            if(Price != null)
            {
                rooms = rooms.Where(x => x.Price == Price);
            }
            if(HotelCity != null)
            {
                rooms = rooms.Where(x => x.Hotel.City.ToLower() == HotelCity.ToLower());
            }
            rooms = rooms.OrderBy(x => x.Price);
            return rooms.ToList<Room>();
        }

        public Room GetRoom(int id)
        {
            try
            {
                var result = db.Rooms.Find(id);
                if (result != null)
                    return result;
                else
                    throw new Exception("Invalid Room Id");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Room PostRoom(Room room)
        {
            try
            {
                room = db.Rooms.Add(room);
                db.SaveChanges();
                return room;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string PutRoom(int Id, Room room)
        {
            throw new NotImplementedException();
        }

        public bool DoesExist(int Id)
        {
            try
            {
                var result = db.Rooms.Find(Id);
                bool exists;
                if (result != null && result.IsActive != false)
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
