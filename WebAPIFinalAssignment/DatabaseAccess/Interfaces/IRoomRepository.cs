using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.Models;
using static DatabaseModels.GlobalVariables;

namespace DatabaseAccess.Interfaces
{
    public interface IRoomRepository
    {
        Room PostRoom(Room room);
        List<Room> GetRooms(int? Pincode, decimal? Price,Categories? Category  ,string HotelCity = null);
        Room GetRoom(int id);
        bool GetAvailability(int Id, DateTime Date);

        bool DoesExist(int Id);
    }
}
