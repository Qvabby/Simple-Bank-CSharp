using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Repositories.Models
{
    public enum OperationType : byte
    {
        [Key]
        Withdraw = 1,
        Deposit = 2,
    }
}
