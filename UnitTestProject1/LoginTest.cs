using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;

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
            //call the method that generates the code
            string code = "generated code";

            bool check = new UsersBL().Login(username, code);

            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void TestInvalidPasswordCredentials()
        {
            string username = "admin";
            string password = "1234";
            //call the method that generates the code
            string code = "generated code";

            bool check = new UsersBL().Login(username, code);

            Assert.AreEqual(check, false);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestInvalidUsernameCredentials()
        {
            string username = "admin1";
            string password = "123";
            //call the method that generates the code
            string code = "generated code";

            bool check = new UsersBL().Login(username, code);

            Assert.AreEqual(check, false);
        }
    }
}
