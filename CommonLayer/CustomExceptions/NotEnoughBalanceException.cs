using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.CustomExceptions
{
    public class NotEnoughBalanceException : Exception
    {
        public NotEnoughBalanceException()
            : base("Not enough balance")
        { }

        public NotEnoughBalanceException(string message)
            : base(message)
        { }

        public NotEnoughBalanceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
