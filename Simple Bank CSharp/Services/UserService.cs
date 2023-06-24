using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Classes;
using Simple_Bank_CSharp.Services.ServiceInterfaces;
using Simple_Bank_CSharp.Implementations;
using Simple_Bank_CSharp.DataBase;

namespace Simple_Bank_CSharp.Services
{
    class UserService : IUserInfoService
    {
        CustomerRepository CustomerRepository = new CustomerRepository();
        SimpleBankDbContext _DbContext = new SimpleBankDbContext();

        public int Login(string email, string pass)
        {
            var customers = _DbContext.Customers;
            foreach (var c in customers)
            {
                if (email == c.Email && pass == c.Password)
                {
                    return c.Id;
                }
            }

            return -1;
        }

        public void Register(Customer customer)
        {
            CustomerRepository.AddCustomer(customer);
        }
    }
}
