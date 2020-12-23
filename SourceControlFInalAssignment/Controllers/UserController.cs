using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using NLog;
using SourceControlFinalAssignment.Context;
using SourceControlFinalAssignment.Models;

namespace SourceControlFinalAssignment.Controllers
{
    public class UserController : Controller
    {
        // Variables for logging.
        // Info level logs are only logged in database
        // Whereas errors are logged in file and in database
        // But i can't figure out why the logs are not inserting in database
        private static Logger fileLogger, dbLogger;

        // Variable for Database transactions
        private ApplicationContext _context;

        public UserController()
        {
            fileLogger = LogManager.GetLogger("fileLogger");
            dbLogger = LogManager.GetLogger("dbLogger");
            _context = new ApplicationContext();
        }

        // GET: User/login
        [HttpGet]
        public ActionResult login()
        { 
            return View();
        }

        // GET: User/register
        [HttpGet]
        public ActionResult register()
        {
            return View();
        }

        // POST: User/login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Login model)
        {
            try
            {
                bool isValid = _context.Logins.Any(x => x.Username == model.Username && x.Password == model.Password);
                if (isValid)
                {
                    var result = _context.Logins.Single(x => x.Username == model.Username && x.Password == model.Password);
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    //Saving User id and username in Session   Object for reference in other Actions
                    Session["UserId"] = result.Id;
                    Session["Username"] = result.Username;
                    fileLogger.Info("{user} logged in at {time} from {ip-address}", result.Username, DateTime.Now.ToString(), System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                    dbLogger.Info("{user} logged in at {time} from {ip-address}", result.Username, DateTime.Now.ToString(), System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                    return RedirectToAction("dashboard");
                }
                dbLogger.Info("{user} attempted login at {time} from {ip-address} with username {username} and password {password}", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_USER"], DateTime.Now.ToString(), System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], model.Username, model.Password);
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            catch(Exception e)
            {
                fileLogger.Error(e, "Error while Logging in");
                dbLogger.Error(e, "Error while Logging in");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message.ToString());
            }
        }

        // POST: User/register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register(UserRegistrationViewModel model)
        {
            try
            {

                Login loginModel = new Login() { Username = model.Email, Password = model.Password};
                User userModel = new User() { Name = model.Name, Email = model.Email, Birthdate = model.Birthdate, City = model.City, Country = model.Country, Gender = model.Gender, Phone = model.Phone  };
                _context.Users.Add(userModel);
                _context.Logins.Add(loginModel);
                _context.SaveChanges();
                dbLogger.Info("{user} registered a new user at {time} from {ip-address} with username {username} and password {password}", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_USER"], DateTime.Now.ToString(), System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], model.Email, model.Password);
                return RedirectToAction("login");
            }
            catch(Exception e)
            {
                fileLogger.Error(e, "Error while Registration");
                dbLogger.Error(e, "Error while Registration");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message.ToString());
            }
        }

        // GET: User/dashboard
        [HttpGet]
        [Authorize]
        public ActionResult dashboard()
        {
            try
            {
                var id = Convert.ToInt64(Session["UserId"]);
                var result = _context.Users.Single(x => x.Id == id); ;
                ViewBag.model = result;
                return View();
            }
            catch(Exception e)
            {
                fileLogger.Error(e, "Error while loading the dashboard");
                dbLogger.Error(e, "Error while loading the dashboard");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message.ToString());
            }
        }

        // GET: User/logout
        [HttpGet]
        [Authorize]
        public ActionResult logout()
        {
            string username = Session["Username"].ToString();
            FormsAuthentication.SignOut();
            dbLogger.Info("{user} logged out at {time}", username, DateTime.Now.ToString());
            return RedirectToAction("login");
        }
    }
}