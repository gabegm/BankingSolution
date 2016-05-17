using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer;

namespace DataLayer
{
    public class TransactionsRepository : ConnectionClass
    {
        public TransactionsRepository() : base()
        { }

        public IQueryable<Transaction> GetTransactions(DateTime From, DateTime To, string Username, string iban)
        {
            return Entity.Transactions.Where(t => t.Date > From && t.Date < To && t.IbanFrom.Equals(iban));
        }

        public void AddTransaction(Transaction t)
        {
            Entity.Transactions.Add(t);
            Entity.SaveChanges();
        }
    }
}
