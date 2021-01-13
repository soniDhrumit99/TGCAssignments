using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatabaseModels.Models;
using BusinessLogic.Interfaces;

/* ------------------------------------------------- * Booking Controller * ------------------------------------------------- */

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Booking")]
    public class BookingController : ApiController
    {
        private readonly IBookingManager _bookingManager;
        public BookingController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        /*
         * Description: Updates the booking date of a room using booking id
         * Parameter:
         *          id<int>         -> Room id
         *          date<DateTime>  -> checking date
         * Return: Object<Bookings>
         * Request: api/Booking/{id}/update?{date}
         * Type: PUT
         * Progress: Testing
         */
        [HttpPut]
        [Route("{id}/updateDate/{date=date}")]
        public IHttpActionResult UpdateBookingDate(int id, DateTime date)
        {
            try
            {
                return Ok(_bookingManager.PutBookingDate(id, date));
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        /*
         * Description: Updates the booking status of a room using booking id
         * Parameter:
         *          id<int>         -> Room id
         *          status<string>  -> Booking Status (Definitive, Cancelled)
         * Return: Object<Bookings>
         * Request: api/Booking/{id}/update?{status}
         * Type: PUT
         * Progress: Testing
         */
        [HttpPut]
        [Route("{id}/updateStatus/{status=status}")]
        public IHttpActionResult UpdateBookingStatus(int id, string status)
        {
            try
            {
                return Ok(_bookingManager.PutBookingStatus(id, status));
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        /*
         * Description: Deletes the booking request using the specified booking id
         * Parameter:
         *          id<int> -> Booking id
         * Return: Object<Bookings>
         * Request: api/Booking/{id}/delete
         * Type: DELETE
         * Progress: Testing
         */
        [HttpDelete]
        [Route("{id}/delete")]
        public IHttpActionResult DeleteBooking(int id)
        {
            try
            {
                return Ok(_bookingManager.DeleteBooking(id));
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
