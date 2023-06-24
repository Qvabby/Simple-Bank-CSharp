using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Utilities
{
    class DateTimeProvider : IDateTimeProvider
    {

        DateTime IDateTimeProvider.Now => DateTime.Now;

        DateTime IDateTimeProvider.UtcNow => DateTime.UtcNow;
    }
}
