using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActionSelectors.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "This is the index action";
        }

        [ActionName("CurrentTime")]
        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("T");
        }

        [ActionName("Time")]
        public string GetTime()
        {
            return TimeString();
        }

        [NonAction]
        public string TimeString()
        {
            return "Current time is " + DateTime.Now.ToString("T");
        }

        public ActionResult Search(string name = "No Name Entered")
        {
            var input = Server.HtmlEncode(name);
            return Content(input);
        }

        [HttpGet]
        public ActionResult Search()
        {
            var input = "Another Search action";
            return Content(input);
        }
    }
}