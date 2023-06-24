using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Commands.Operations;

namespace Simple_Bank_CSharp.Services
{
    interface IOperationService
    {
        /// <summary>
        /// Money Withdraw Operation (ფულის ჩამოჭრის ოპერაცია)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        long Withdraw(WithdrawByAccountId command);

        /// <summary>
        /// Money Deposit Operation (ფულის ჩარიცხვის ოპერაცია)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        long Deposit(DepositByAccountId command);
    }
}
