using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Commands
{
    class OperationCommand
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime HappenedAt { get; set; }
    }
}
