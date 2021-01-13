using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DatabaseModels.Models;
using DatabaseAccess.Interfaces;

namespace BusinessLogic.Classes
{
    public class HotelManager : IHotelManager
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelManager(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public List<Hotels> GetAllHotels()
        {
            try
            {
                return _hotelRepository.GetAllHotels();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool isValid(Hotels hotel)
        {
            bool valid;
            if(hotel.Name == null || hotel.Address == null || hotel.City == null || hotel.ContactNumber == 0 
                || hotel.ContactPerson == null || hotel.Pincode == 0 || hotel.CreatedBy == null || hotel.UpdatedBy == null)
            {
                valid = false;
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        public bool HotelExists(int Id)
        {
            try
            {
                return _hotelRepository.DoesExist(Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Hotels PostHotel(Hotels hotel)
        {
            try
            {
                if (isValid(hotel))
                {
                    hotel.IsActive = true;
                    hotel.CreatedDate = DateTime.UtcNow;
                    hotel.UpdatedDate = DateTime.UtcNow;
                    return _hotelRepository.PostHotel(hotel);
                }
                else
                {
                    throw new Exception("Insufficient Hotel details.");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
