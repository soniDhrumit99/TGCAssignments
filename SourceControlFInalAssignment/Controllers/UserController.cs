using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SourceControlFInalAssignment.Context;
using SourceControlFInalAssignment.Models;

namespace SourceControlFInalAssignment.Controllers
{
    public class UserController : Controller
    {
        private UserContext _context;
        //private logger = 

        public UserController()
        {
            _context = new UserContext(); 
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            try { 
                var user = _context.Logins.Single(x => x.Name == model.Name && x.Password == model.Password);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("error-msg", "Invalid Credentials");
            }
            return View();
        }

        [HttpGet]
        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register(UserModel model)
        {
            return RedirectToAction("dashboard");
        }

        [HttpGet]
        public ActionResult dashboard()
        {
            return View("Index");
        }
    }
}