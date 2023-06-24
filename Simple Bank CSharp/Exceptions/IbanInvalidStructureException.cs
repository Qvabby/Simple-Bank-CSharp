using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Exceptions
{
    class IbanInvalidStructureException : DomainException
    {
        public IbanInvalidStructureException(string value) : base($"Iban has Invalid structure - {value}")
        {

        }
    }
}
