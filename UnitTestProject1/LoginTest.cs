using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void TestValidCredentials()
        {
            //paste the algorithm that generates the code

            string username = "admin";
            string password = "123";
            String date = DateTime.Now.ToString("g");

            string code = Encrypt(password, date);
            string generatedcode = new BankingAPI.Controllers.UsersApiController().Encrypt(password, date);
            bool check = new UsersBL().Login(username, password);
            bool check2 = code == generatedcode;
            Assert.AreEqual(check, true);

            Assert.AreEqual(check2, true);
        }

        [TestMethod]
        public void TestInvalidPasswordCredentials()
        {
            //paste teh algorithm that generates the code

            string username = "admin";
            string password = "789";

            String date = DateTime.Now.ToString("g");

            string AllString = password + date;
            if (string.IsNullOrEmpty(AllString)) throw new ArgumentNullException();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(AllString);
            buffer = System.Security.Cryptography.SHA512.Create().ComputeHash(buffer);
            return; // strip padding and special characters


            string code = Regex.Replace(Convert.ToBase64String(buffer).Substring(0, 86), "[^0-9a-zA-Z]+", "");

            string generatedcode = new BankingSolutionApi.Controllers.UsersApiController().Encrypt(password, date);
            bool check = new UsersBL().Login(username, password);
            bool check2 = code == generatedcode;
            Assert.AreEqual(check, true);

            Assert.AreEqual(check2, true);
        }

        [TestMethod]
        public void TestInvalidUsernameCredentials()
        {
            //paste teh algorithm that generates the code

            string username = "toni";
            string password = "123";

            String date = DateTime.Now.ToString("g");

            string AllString = password + date;
            if (string.IsNullOrEmpty(AllString)) throw new ArgumentNullException();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(AllString);
            buffer = System.Security.Cryptography.SHA512.Create().ComputeHash(buffer);
            return; // strip padding and special characters


            string code = Regex.Replace(Convert.ToBase64String(buffer).Substring(0, 86), "[^0-9a-zA-Z]+", "");

            string generatedcode = new BankingSolutionApi.Controllers.UsersApiController().Encrypt(password, date);
            bool check = new UsersBL().Login(username, password);
            bool check2 = code == generatedcode;
            Assert.AreEqual(check, true);

            Assert.AreEqual(check2, true);
        }

        public static string Encrypt(string Password, string Date)
        {
            string AllString = Password + Date;
            if (string.IsNullOrEmpty(AllString)) throw new ArgumentNullException();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(AllString);
            buffer = System.Security.Cryptography.SHA512.Create().ComputeHash(buffer);
            return Regex.Replace(Convert.ToBase64String(buffer).Substring(0, 86), "[^0-9a-zA-Z]+", ""); // strip padding and special characters
        }
    }
}
