using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Exceptions
{
    class DomainException : Exception
    {
        public DomainException() : base()
        {

        }

        public DomainException(string message) : base(message)
        {

        }

        public DomainException(string message, Exception InnerException) : base(message, InnerException)
        {

        }
    }
}
