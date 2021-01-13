using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.Models;

namespace BusinessLogic.Interfaces
{
    public interface IRoomManager
    {
        Room PostRoom(Room room);
        List<Room> GetRooms(int? Pincode, decimal? Price, string Category = null, string HotelCity = null);
        Room GetRoom(int id);
        bool GetAvailability(int Id, DateTime Date);
        Bookings BookRoom(int id, DateTime date);

        bool RoomExists(int Id);
        bool IsValid(Room room);
    }
}
