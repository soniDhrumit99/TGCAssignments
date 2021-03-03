using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using TestingAssignment.Context;
using TestingAssignment.Models;

namespace TestingAssignment.Controllers
{
    public class PassengersController : ApiController
    {
        private passengerDbContext db = new passengerDbContext();

        private List<Passenger> passengers = new List<Passenger>();

        public PassengersController()
        {
            passengers.Add(new Passenger(1, "John", "Doe", "8987968584"));
            passengers.Add(new Passenger(2, "Joseph", "Dooley", "8987968584"));
            passengers.Add(new Passenger(3, "Kirk", "Allen", "3457968584"));
            passengers.Add(new Passenger(4, "Mark", "Rober", "8987345584"));
            passengers.Add(new Passenger(5, "Bill", "Clinton", "8987345584"));
            passengers.Add(new Passenger(6, "Bob", "Builder", "8987968345"));
        }

        // GET: api/Passengers
        public JsonResult<List<Passenger>> GetPassengers()
        {
            return Json(passengers);
        }

        // GET: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public JsonResult<Passenger> GetPassenger(int id)
        {
            return Json(passengers[id-1]);
        }

        // PUT: api/Passengers/5
        public JsonResult<Passenger> PutPassenger(int id, Passenger passenger)
        {
            return Json(passengers[id-1]);
        }

        // POST: api/Passengers
        public JsonResult<Passenger> PostPassenger(Passenger passenger)
        { 
            passengers.Add(passenger);
            return Json(passengers[passengers.IndexOf(passenger)]);
        }

        // DELETE: api/Passengers/5
        public JsonResult<Passenger> DeletePassenger(int id)
        {
            Passenger deletedPassenger = passengers[id-1];
            passengers.Remove(passengers[id]);
            return Json(deletedPassenger);
        }
    }
}