using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Classes;

namespace Simple_Bank_CSharp.Repositories
{
    interface ICustomerRepository
    {
        public Customer GetCustomerById(int id);

        public int AddCustomer(Customer customer);

        public bool UpdateCustomer(Customer customer);

        public bool DeleteCustomer(int Id);
    }
}
