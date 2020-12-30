using UnitTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace UnitTesting.Controllers
{
    public class EmployeeController : Controller
    {
        [NonAction]

        public List<Employee> GetEmployeeList()
        {
            return new List<Employee>{
            new Employee{
               ID = 1,
               Name = "Allan",
               JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
               Age = 23
            },

            new Employee{
               ID = 2,
               Name = "Carson",
               JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
               Age = 45
            },

            new Employee{
               ID = 3,
               Name = "Carson",
               JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
               Age = 37
            },

            new Employee{
               ID = 4,
               Name = "Laura",
               JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
               Age = 26
            },
         };
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Employees()
        {
            var employees = from e in GetEmployeeList()
                            orderby e.ID
                            select e;
            return View(employees);
        }
    }
}