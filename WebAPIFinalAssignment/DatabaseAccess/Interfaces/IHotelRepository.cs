using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.Models;

namespace DatabaseAccess.Interfaces
{
    public interface IHotelRepository
    {
        Hotels PostHotel(Hotels hotel);
        List<Hotels> GetAllHotels();
        bool DoesExist(int Id);
    }
}
