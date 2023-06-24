using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Classes;
using Simple_Bank_CSharp.Repositories.Models;

namespace Simple_Bank_CSharp.Services.ServiceInterfaces
{
    interface IUserInfoService
    {
        public void Register(Customer customer);
        public int Login(string email, string pass);

    }
}
