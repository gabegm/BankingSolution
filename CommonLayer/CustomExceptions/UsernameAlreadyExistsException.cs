using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.CustomExceptions
{
    public class UsernameAlreadyExistsException: Exception
    {
        public UsernameAlreadyExistsException() : base("Username already exists")
        {

        }

        public UsernameAlreadyExistsException(string message)
            : base(message)
        {

        }

        public UsernameAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
