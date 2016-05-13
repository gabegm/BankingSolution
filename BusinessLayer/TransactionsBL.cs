using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using CommonLayer;

namespace BusinessLayer
{
    public class TransactionsBL
    {
        public IQueryable<Transaction> GetTransactions(DateTime From, DateTime To, string Username)
        {
            //validation
            return new TransactionsRepository().GetTransactions(From, To, Username);
        }

        public void AddTransaction(Transaction t)
        {
            new TransactionsRepository().AddTransaction(t);
        }
    }
}
