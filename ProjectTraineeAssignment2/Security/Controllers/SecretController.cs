using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Security.Controllers
{
    [Authorize(Users = "dhrumit.soni@gmail.com")]
    public class SecretController : Controller
    {
        // GET: Secret
        //[Authorize]
        public ContentResult Secret()
        {
            return Content("Secret informations here");
        }

        [AllowAnonymous]
        public ContentResult PublicInfo()
        {
            return Content("Public informations here");
        }
    }
}