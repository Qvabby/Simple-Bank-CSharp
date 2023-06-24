using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Classes;

namespace Simple_Bank_CSharp.Repositories
{
    interface IAccountRepository
    {
        public Account GetAccountById(int id);

        public int AddAccount(Account customer);

        public bool UpdateAccount(Account customer);

        public bool DelateAccount(int Id);

    }
}
