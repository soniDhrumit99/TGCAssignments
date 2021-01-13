using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DatabaseAccess.Interfaces;
using DatabaseModels.Models;
using static DatabaseModels.GlobalVariables;

namespace BusinessLogic.Classes
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IHotelRepository _hotelRepository;

        public RoomManager(IRoomRepository roomRepository, IBookingRepository bookingRepository, IHotelRepository hotelRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _hotelRepository = hotelRepository;
        }

        public bool GetAvailability(int Id, DateTime Date)
        {
            try
            {
                if (RoomExists(Id))
                    return _roomRepository.GetAvailability(Id, Date.Date);
                else
                    throw new Exception("Invalid Room Id");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Room> GetRooms(int? Pincode, decimal? Price, string Category = null, string HotelCity = null)
        {
            try
            {
                if(Category != null)
                {
                    switch (Category.ToLower())
                    {
                        case "small":
                            return _roomRepository.GetRooms(Pincode, Price, Categories.Small, HotelCity);
                        case "medium":
                            return _roomRepository.GetRooms(Pincode, Price, Categories.Medium, HotelCity);
                        case "large":
                            return _roomRepository.GetRooms(Pincode, Price, Categories.Large, HotelCity);
                        default:
                            throw new Exception("Invalid Category for getting Rooms");
                    }
                }
                else
                {
                    return _roomRepository.GetRooms(Pincode, Price, null, HotelCity);
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Room GetRoom(int id)
        {
            try
            {
                return _roomRepository.GetRoom(id);

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
                if (IsValid(room))
                {
                    if (_hotelRepository.DoesExist(room.HotelId))
                    {
                        room.CreatedDate = DateTime.UtcNow;
                        room.UpdatedDate = DateTime.UtcNow;
                        room.IsActive = true;
                        return _roomRepository.PostRoom(room);
                    }
                    else
                    {
                        throw new Exception("Invalid Hotel Id");
                    }
                }
                else
                {
                    throw new Exception("Insufficient Room details");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Bookings BookRoom(int id, DateTime date)
        {
            try
            {
                if (RoomExists(id))
                {
                    if(GetAvailability(id, date))
                    {
                        Bookings newBooking = new Bookings
                        {
                            BookingDate = date.Date,
                            RoomId = id,
                            Status = BookingStatus.Optional
                        };
                        return _bookingRepository.PostBooking(newBooking);
                    }
                    else
                    {
                        throw new Exception("Room is already booked for the specified date.");
                    }
                }
                else
                {
                    throw new Exception("Invalid Room Id");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool RoomExists(int Id)
        {
            try
            {
                return _roomRepository.DoesExist(Id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool IsValid(Room room)
        {
            bool valid;
            if (room.HotelId == -1 || room.Name == null || room.Category == null || room.CreatedBy == null
                || room.Price == -1 || room.UpdatedBy == null)
            {
                valid = false;
            }
            else
            {
                valid = true;
            }
            return valid;
        }
    }
}
