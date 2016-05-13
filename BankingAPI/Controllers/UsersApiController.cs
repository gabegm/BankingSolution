using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonLayer;
using BusinessLayer;
using CommonLayer.CustomExceptions;
using System.Web.Http.Cors;

namespace BankingAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersApiController : ApiController
    {
        public HttpResponseMessage Get()
        {
            HttpResponseMessage message;
            try
            {
                List<User> users = new UsersBL().GetUsers().ToList();
                //i am returning a list of anonymous objects on the fly
                //because an anonymous is serialized without any problems

                var result = from u in users
                             select new { Username = u.Username, FirstName = u.FirstName, Surname = u.Surname };
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

        public HttpResponseMessage Get(string keyword)
        {
            HttpResponseMessage message;

            try
            {
                List<User> users = new UsersBL().GetUsers(keyword).ToList();
                //i am returning a list of anonymous objects on the fly
                //because an anonymous is serialized without any problems
                var result = from u in users
                             select new { Username = u.Username, FirstName = u.FirstName, Surname = u.Surname };
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

        public HttpResponseMessage Get(string firstname, string surname)
        {
            HttpResponseMessage message;
            try
            {
                List<User> users = new UsersBL().GetUsers(firstname, surname).ToList();
                //i am returning a list of anonymous objects on the fly
                //because an anonymous is serialized without any problems
                var result = from u in users
                             select new { Username = u.Username, FirstName = u.FirstName, Surname = u.Surname };
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

        public HttpResponseMessage GetUser(string username)
        {
            HttpResponseMessage message;
            try
            {
                User u = new UsersBL().GetUser(username);
                if (u != null)
                {
                    var myuser = new { Username = u.Username, FirstName = u.FirstName };
                    message = Request.CreateResponse(HttpStatusCode.OK, myuser);
                }
                else
                {
                    message = Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    new HttpError("Error occurred. Please Try again later")
                    );
            }

            return message;
        }

        public HttpResponseMessage RegisterUser([FromUri]User myUser)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                new UsersBL().RegisterUser(myUser);
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (UsernameAlreadyExistsException ex)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
            }

            catch (Exception)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,
                    "Error Occurred in database. Please contact admin on bankadmin@gmail.com");
            }

            return message;
        }

        public HttpResponseMessage RegisterUser([FromBody] string username)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                //    new UsersBL().RegisterUser(myUser);
                message = Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (UsernameAlreadyExistsException ex)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
            }

            catch (Exception)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,
                    "Error Occurred in database. Please contact admin on bankadmin@gmail.com");
            }

            return message;
        }

        /*

         * http://localhost:51295/api/UsersApi/GetAuthenticateUser/?username=admin&password=123

         */
        public HttpResponseMessage GetAuthenticateUser(string username, string password)
        {
            HttpResponseMessage message = new HttpResponseMessage();

            try
            {
                //if (Encrypt(new UsersBL().GetUser(username).Password, DateTime.Now.ToString("g")) == password)
                if(new UsersBL().GetUser(username).Password == password)
                {
                    message = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
                }
                else
                {
                    message = Request.CreateResponse<bool>(HttpStatusCode.NotAcceptable, false);
                }
            }
            catch (Exception)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotFound,
                     new HttpError("Error occurred. Please Try again later")
                     );
            }

            return message;
        }

        public static string Encrypt(string Password, string Date)
        {
            string AllString = Password + Date;
            if (string.IsNullOrEmpty(AllString)) throw new ArgumentNullException();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(AllString);
            buffer = System.Security.Cryptography.SHA512.Create().ComputeHash(buffer);
            return Convert.ToBase64String(buffer).Substring(0, 86); // strip padding
        }
    }
}