using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLayer;
using BusinessLayer;

namespace UnitTestProject1
{
    [TestClass]
    public class CreateAccountTest
    {
        [TestMethod]
        public void TestCreateAccount()
        {
            BankAccount b = new BankAccount();
            b.Iban = Guid.NewGuid().ToString();
            b.Currency_Fk = "EUR";
            b.Balance = 200;
            b.Username_Fk = "admin";
            b.DateOpened = DateTime.Today;
            b.AccountType_Fk = 1;
            b.AccountNumber = 0;

            BankAccountsBL bankAccountsBL = new BankAccountsBL();

            bankAccountsBL.OpenNewBankAccount(b);

            Assert.IsNotNull(bankAccountsBL.GetBankAccounts(b.Username_Fk, b.AccountType_Fk));
        }
    }
}
