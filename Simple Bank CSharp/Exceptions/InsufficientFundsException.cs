using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Exceptions
{
    class InsufficientFundsException : DomainException
    {
        public InsufficientFundsException(int accountId) : base($"Insufficient Funds On Account (ID) : {accountId}")
        {

        }
    }
}
