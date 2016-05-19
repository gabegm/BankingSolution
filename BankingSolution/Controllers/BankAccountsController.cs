﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer;

namespace BankingSolution.Controllers
{
    public class BankAccountsController : Controller
    {
        // GET: BankAccounts
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Transfer()
        {
            return View(new BankAccountsBL().GetUserBankAccounts(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        public async Task<ActionResult> Transfer1()
        {
            HttpClient client = new HttpClient();

            var url = "http://localhost:51901/api/BankAccountsApi/Transfer";//?ibanFrom=" + ibanFrom + "&ibanTo=" + ibanTo + "&amount=" + amount;

            var postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("iban", "123"));

            HttpContent content = new FormUrlEncodedContent(postData);

            var response = await client.PostAsync(url, content);

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Transactions()
        {
            return View();
        }
    }
}