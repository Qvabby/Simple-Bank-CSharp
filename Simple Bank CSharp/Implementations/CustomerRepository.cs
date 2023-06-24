using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Classes;
using Simple_Bank_CSharp.Repositories;
using Simple_Bank_CSharp.DataBase;
using Simple_Bank_CSharp.Utilities;

namespace Simple_Bank_CSharp.Implementations
{
    class CustomerRepository : ICustomerRepository
    {
        private readonly SimpleBankDbContext _DbContext;
        private Logger Logger;

        public CustomerRepository()
        {
            _DbContext = new SimpleBankDbContext();
        }

        public int AddCustomer(Customer customer)
        {
            try
            {
                var result = _DbContext.Customers.Add(customer);
                _DbContext.SaveChanges();
                return result.Entity.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return -1;
            }
            
        }

        public bool DeleteCustomer(int Id)
        {
            try
            {
                var customer = _DbContext.Customers.Find(Id);
                _DbContext.Customers.Remove(customer);
                return _DbContext.SaveChanges() != 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return false;
            }
            
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                var customer = _DbContext.Customers.Find(id);
                if (customer != null)
                {
                    return new Customer()
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        IdentityNumber = customer.IdentityNumber,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email,
                        Type = customer.Type,
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return null;
            }
            
        }

        public bool UpdateCustomer(Customer customer)
        {
            //Id,Name,IdentityNumber,PhoneNumber,Email,Type, password
            try
            {
                var CustToUp = _DbContext.Customers.Find(customer.Id);
                CustToUp.Name = customer.Name;
                CustToUp.IdentityNumber = customer.IdentityNumber;
                CustToUp.PhoneNumber = customer.PhoneNumber;
                CustToUp.Email = customer.Email;
                CustToUp.Type = customer.Type;
                CustToUp.Password = customer.Password;
                return _DbContext.SaveChanges() != 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return false;
            }
            
        }
    }
}
