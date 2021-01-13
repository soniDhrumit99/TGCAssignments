using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccess.Context;
using DatabaseAccess.Interfaces;
using DatabaseModels.Models;

namespace DatabaseAccess.Classes
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext db;

        public HotelRepository()
        {
            db = new AppDbContext();
        }

        public List<Hotels> GetAllHotels()
        {
            try
            {
                return db.Hotels.OrderBy(x => x.Name).ToList<Hotels>();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Hotels PostHotel(Hotels hotel)
        {
            try
            {
                hotel = db.Hotels.Add(hotel);
                db.SaveChanges();
                return hotel;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool DoesExist(int Id)
        {
            try
            {
                var result = db.Hotels.Find(Id);
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
