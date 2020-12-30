using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controllers.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public string GetAllCustomers()
        {
            return @"<ul>
                <li>Mark Ronson</li>
                <li>Brenny Markson</li>
                <li>Shaw Beeman</li>
                <li>Hugh Grant</li>
                </ul>";
        }
    }
}