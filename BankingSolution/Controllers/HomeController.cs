using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankingSolution.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password, string name, string surname, string email)
        {
            //you process your request e.g. you call the webapi method
            ViewBag.Message = "User was registered successfully";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}