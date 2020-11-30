using System.Web.Mvc;
using SourceControlAssignment1.Models;

namespace SourceControlAssignment1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        //POST: EmployeeDetails
        [HttpPost]
        public ActionResult EmployeeDetails(Employee e)
        {
            if (ModelState.IsValid)
            {
                ViewBag.name = e.Name;
                ViewBag.email = e.Email;
                ViewBag.age = e.Age;
                ViewBag.dept = e.Dept;
                return View("Index");
            }
            else
            {
                ViewBag.name = "No Data";
                ViewBag.email = "No Data";
                ViewBag.age = 0;
                ViewBag.dept = "No Data";
                return View("Index");
            }
        }
    }
}