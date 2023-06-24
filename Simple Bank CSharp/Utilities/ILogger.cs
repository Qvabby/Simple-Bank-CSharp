using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Utilities
{
    interface ILogger
    {
        public void LogError(Exception e);
    }
}