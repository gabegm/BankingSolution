using BusinessLayer;
using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CommonLayer.CustomExceptions;

namespace BankingAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BankAccountsApiController : ApiController
    {
        /// <summary>
        /// this would get all the account types from the database
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAccountTypes()
        {
            double rate = new BankAccountsBL().GetCurrencyRate("EUR", "USD");

            HttpResponseMessage message;
            try
            {
                List<AccountType> accounts = new BankAccountsBL().GetAccountTypes().ToList();
                //i am returning a list of anonymous objects on the fly
                //because an anonymous is serialized without any problems
                var result = from a in accounts
                             select new { Id = a.Id, Name = a.Name };

                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    new HttpError("Error occurred. Please Try again later")
                    );
            }
            return message;
        }

        public HttpResponseMessage GetBankAccounts(string username, int id)
        {
            HttpResponseMessage message;
            try
            {
                List<BankAccount> accounts = new BankAccountsBL().GetBankAccounts(username, id).ToList();
                //i am returning a list of anonymous objects on the fly
                //because an anonymous is serialized without any problems
                var result = from a in accounts
                             select new { Iban = a.Iban, Balance = a.Balance, Currency = a.Currency_Fk, DateOpened = a.DateOpened.ToShortDateString() };

                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    new HttpError("Error occurred. Please Try again later")
                    );
            }
            return message;
        }

        [HttpGet]
        public HttpResponseMessage OpenNewBankAccount(string username, decimal balance, string currency, int type, string from, int duration)
        {
            //http://localhost:51901/api/bankAccountsApi/OpenNewBankAccount/?username=admin&balance=1111&currency=USD&type=1

            int? dur = duration;

            if(dur == -1)
            {
                dur = null;
            }

            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                BankAccount b = new BankAccount()
                {
                    Username_Fk = username,
                    Balance = balance,
                    Currency_Fk = currency,
                    AccountType_Fk = type,
                    DateOpened = DateTime.Now,
                    Iban = Guid.NewGuid().ToString(),
                    Duration = dur
                };

                BankAccountsBL bankAccountsBL = new BankAccountsBL();

                bankAccountsBL.OpenNewBankAccount(b);

                bankAccountsBL.TransferFunds(from, b.Iban, balance);

                TransactionsBL transactionsBL = new TransactionsBL();

                Transaction t = new Transaction()
                {
                    Username = username,
                    Amount = null,
                    Currency_Fk = currency,
                    Date = DateTime.Today,
                    IbanFrom = b.Iban,
                    Description = "Account opened"
                };

                transactionsBL.AddTransaction(t);

                Transaction t1 = new Transaction()
                {
                    Username = username,
                    Amount = balance,
                    Currency_Fk = currency,
                    Date = DateTime.Today,
                    IbanFrom = b.Iban,
                    IbanTo = from,
                    Description = "Funds transfered"
                };

                transactionsBL.AddTransaction(t1);

                message = Request.CreateResponse(HttpStatusCode.Accepted, "OK");
            }
            catch
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Error occurred while inputting data");
            }
            return message;
        }

        [HttpGet]
        public HttpResponseMessage TransferFunds(string ibanFrom, string ibanTo, decimal amount)
        {
            //http://localhost:63723/api/BankAccountsApi/TransferFunds/?ibanFrom=56c61dfc-7c2a-41a6-8d50-327ec264bd03&ibanTo=2&amount=10

            HttpResponseMessage message = new HttpResponseMessage();

            try
            {
                BankAccountsBL bankAccountsBL = new BankAccountsBL();

                bankAccountsBL.TransferFunds(ibanFrom, ibanTo, amount);

                TransactionsBL transactionsBL = new TransactionsBL();

                Transaction t = new Transaction()
                {
                    //Username = username,
                    Amount = amount,
                    //Currency_Fk = currency,
                    Date = DateTime.Today,
                    IbanFrom = ibanTo,
                    IbanTo = ibanFrom,
                    Description = "Funds transfered"
                };

                transactionsBL.AddTransaction(t);

                message = Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (NotEnoughBalanceException ex)
            {
                throw ex;
            }
            catch
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Error occurred while inputting data");
            }
            return message;
        }

        [HttpPost]
        public HttpResponseMessage Transfer(string iban)
        {
            HttpResponseMessage message = new HttpResponseMessage();

            try
            {
                message = Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (NotEnoughBalanceException ex)
            {
                throw ex;
            }
            catch
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Error occurred while inputting data");
            }
            return message;
        }

        public HttpResponseMessage GetTransactions(string username, string iban, DateTime from, DateTime to)
        {
            HttpResponseMessage message;
            try
            {
                List<Transaction> transactions = new TransactionsBL().GetTransactions(from, to, username, iban).ToList();
                //i am returning a list of anonymous objects on the fly
                //because an anonymous is serialized without any problems
                var result = from t in transactions
                             select new { Username = t.Username, Amount = t.Amount, Currency = t.Currency_Fk, Date = DateTime.Today.ToString("dd-MM-yyy"), IbanTo = t.IbanTo, Description = t.Description, IbanFrom = t.IbanFrom };

                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    new HttpError("Error occurred. Please Try again later")
                    );
            }
            return message;
        }

        [HttpGet]
        public HttpResponseMessage AddTransaction(string username, decimal amount, string currency, string description, string ibanFrom)
        {
            //http://localhost:51901/api/bankAccountsApi/OpenNewBankAccount/?username=admin&balance=1111&currency=USD&type=1

            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                Transaction t = new Transaction()
                {
                    Username = username,
                    Amount = amount,
                    Currency_Fk = currency,
                    Date = DateTime.Today,
                    IbanTo = null,
                    Description = "Opened new bank account",
                    IbanFrom = ibanFrom
                };

                new TransactionsBL().AddTransaction(t);

                message = Request.CreateResponse(HttpStatusCode.Accepted, "OK");
            }
            catch
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Error occurred while inputting data");
            }
            return message;
        }
    }
}