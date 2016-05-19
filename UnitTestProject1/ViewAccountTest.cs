using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CommonLayer;
using BusinessLayer;

namespace UnitTestProject1
{
    [TestClass]
    public class ViewAccountTest
    {
        [TestMethod]
        public void TestGetBankAccounts()
        {
            string username = "admin";
            int accountTypeId = 1;

            IQueryable<BankAccount> bankAccounts = new BankAccountsBL().GetBankAccounts(username, accountTypeId);

            Assert.IsNotNull(bankAccounts);
        }
    }
}
