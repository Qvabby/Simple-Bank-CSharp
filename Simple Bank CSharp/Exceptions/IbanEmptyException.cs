using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Exceptions
{
    class IbanEmptyException : DomainException
    {
        public IbanEmptyException(string accIban) : base($"Iban of the account is empty or there is white space in it - {accIban}")
        {

        }
    }
}
