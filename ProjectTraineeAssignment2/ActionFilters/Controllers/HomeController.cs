using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionFilters.Filters;

namespace ActionFilters.Controllers
{
    [MyLogActionFilter]
    public class HomeController : Controller
    {
        // GET: Home
        [OutputCache(Duration = 15)]
        public string Index()
        {
            return "This is Home Controller";
        }

        // GET: Time
        [OutputCache(Duration = 20)]
        public string Time()
        {
            return DateTime.Now.ToString("T");
        }
    }
}