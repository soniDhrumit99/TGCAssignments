using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatabaseModels.Models;
using BusinessLogic.Interfaces;

/* -------------------------------------------------- * Rooms Controller * -------------------------------------------------- */

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Room")]
    public class RoomController : ApiController
    {
        private readonly IRoomManager _roomManager;
        public RoomController(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }


        /*
         * Description: Checks the availablity of a room on certain date
         * Parameter:
         *          id<int>         -> Room id
         *          date<DateTime>  -> checking date
         * Return: bool (True or False)
         * Request: api/Room/{id}/availabilty?{date=date}
         * Type: GET
         * Progress: Done
         */
        [HttpGet]
        [Route("{id}/availability/{date=date}")]
        public IHttpActionResult GetRoomAvailablity(int id, DateTime date)
        {                   
            try
            {
                return Ok(_roomManager.GetAvailability(id, date));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /*
         * Description: Gets the list of all the rooms, filtered by search parameters, if any
         * Parameter:
         *          hotelCity<string>   -> City where Hotel is based
         *          pincode<int>        -> Pincode of the Hotel
         *          price<decimal>      -> Per night price of the Room
         *          category<string>       -> Category of the room (Small, Medium, Large)
         * Return: List<Room>
         * Request: api/Room/all?{hotelCity=hotelCity}&{pincode=pincode}&{price=price}&{category=category}
         * Type: GET
         * Progress: Done
         */
        [HttpGet]
        [Route("all/{hotelCity=}/{pincode=pincode}/{price=price}/{category=}")]
        public IHttpActionResult GetRooms(int? pincode, decimal? price, string category = null, string hotelCity = null)
        {
            try
            {
                List<Room> rooms = _roomManager.GetRooms(pincode, price, category, hotelCity);
                if (rooms.Count == 0)
                    return Content(HttpStatusCode.NoContent, "There are no Rooms with specified conditions");
                else
                    return Ok(rooms);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        /*
         * Description: Books the specified room for the specified date with Booking status as "Optional".
         * Parameter:
         *          id<int>         -> Room id
         *          date<DateTime>  -> Booking date
         * Return: Object<Bookings>
         * Request: api/Room/{id}/book?{date}
         * Type: POST
         * Progress: Testing
         */
        [HttpPost]
        [Route("{id}/book/{date=date}")]
        public IHttpActionResult BookRoom(int id, DateTime date)
        {
            try
            {
                return Ok(_roomManager.BookRoom(id, date));
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        /*
         * Description: Adds a new Room to the database
         * Parameter:
         *           room<Room> -> New Room details
         *           {
         *              Name<string>: "",
         *              Category<Categories>: ,
         *              Price<decimal>: ,
         *              HotelId<int>: 
         *              
         *           }
         * Return: Object<Room>
         * Request: api/Room/add
         * Type: POST
         * Progress: Testing
         */
        [HttpPost]
        [Route("add")]
        public IHttpActionResult PostRoom(Room room)
        {
            try
            {
                room.CreatedBy = User.Identity.Name;
                room.UpdatedBy = User.Identity.Name;
                return Ok(_roomManager.PostRoom(room));
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}