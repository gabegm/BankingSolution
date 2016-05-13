using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer;

namespace DataLayer
{
    public class BankAccountsRepository : ConnectionClass
    {
        public BankAccountsRepository() : base()
        { }

        public IQueryable<AccountType> GetAccountTypes()
        {
            return Entity.AccountTypes;
        }

        public IQueryable<BankAccount> GetBankAccounts(string username, int accountTypeId)
        {
            return Entity.BankAccounts.Where(x => x.Username_Fk == username && x.AccountType_Fk == accountTypeId);
        }

        public void OpenNewBankAccount(BankAccount b)
        {
            Entity.BankAccounts.Add(b);
            Entity.SaveChanges();
        }

        public BankAccount GetBankAccount(string iban)
        {
            return Entity.BankAccounts.SingleOrDefault(x => x.Iban == iban);
        }

        public void Withdraw(string iban, decimal amount)
        {
            GetBankAccount(iban).Balance -= amount;
            Entity.SaveChanges();
        }

        public bool DoesAccountHaveEnoughFunds(string iban, decimal amount)
        {
            return GetBankAccount(iban).Balance >= amount ? true : false;
        }

        public void Deposit(string iban, decimal amount)
        {
            GetBankAccount(iban).Balance += amount;
            Entity.SaveChanges();
        }
    }
}
