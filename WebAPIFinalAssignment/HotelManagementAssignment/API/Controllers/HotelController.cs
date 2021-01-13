using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatabaseModels.Models;
using BusinessLogic.Interfaces;

/* -------------------------------------------------- * Hotel Controller * -------------------------------------------------- */

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Hotel")]
    public class HotelController : ApiController
    {
        private readonly IHotelManager _hotelManager;
        public HotelController(IHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }


        /*
         * Description: Gets the list of all the Hotels sorted by alphabetical order
         * Return: List<Hotels>
         * Request: api/Hotel/all
         * Type: GET
         */
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetHotels()
        {
            try
            {
                return Ok(_hotelManager.GetAllHotels());
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }


        /*
         * Description: Adds a new Hotel to the database
         * Parameter:
         *          hotel<Hotels> -> New Hotel details
         *          {
         *              Name<string>: "",
         *              Address<string>: "",
         *              City<string>: "",
         *              Pincode<int>: ,
         *              ContactNumber<long>: ,
         *              ContactPerson<string>: "",
         *              Website<string>: "",        OPTIONAL
         *              Facebook<string>: "",       OPTIONAL
         *              Twitter<string>: "",        OPTIONAL
         *              
         *          }
         * Return: Object<Hotel>
         * Request: api/Hotel/add
         * Type: POST
         */
        [HttpPost]
        [Route("add")]
        public IHttpActionResult PostHotel(Hotels hotel)
        {
            try
            {
                hotel.CreatedBy = User.Identity.Name;
                hotel.UpdatedBy = User.Identity.Name;
                return Ok(_hotelManager.PostHotel(hotel));
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
