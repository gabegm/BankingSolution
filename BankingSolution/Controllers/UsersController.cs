using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankingSolution.Controllers
{
    public class UsersController : Controller
    {
        // GET: /Users/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            //Login processing

            //how to call a Web Api method from the controller (and not from ajax)

            HttpClient client = new HttpClient();

            //http://localhost:51901/api/UsersApi/GetAuthenticateUser/?username=admin&password=123

            client.BaseAddress = new Uri("http://localhost:51901/");

            string result = await client.GetStringAsync("api/UsersApi/GetAuthenticateUser/?username=" + username + "&password=" + password);

            if (result == "true")
            {
                FormsAuthentication.SetAuthCookie(username, true); //Context
                return RedirectToAction("Index", "BankAccounts");
            }
            else
            {
                ViewBag.Message = "Login failed";
            }

            return View();
        }
    }
}