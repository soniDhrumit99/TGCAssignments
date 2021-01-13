using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.Models;

namespace BusinessLogic.Interfaces
{
    public interface IHotelManager
    {
        Hotels PostHotel(Hotels hotel);
        List<Hotels> GetAllHotels();
        bool isValid(Hotels hotel);
        bool HotelExists(int Id);
    }
}
