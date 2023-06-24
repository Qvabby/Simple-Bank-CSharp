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
    class AccountRepository : IAccountRepository
    {

        private readonly SimpleBankDbContext _DbContext;
        private Logger Logger;
        public AccountRepository()
        {
            _DbContext = new SimpleBankDbContext();
        }
        public Account GetAccountById(int id)
        {
            try
            {
                var account = _DbContext.Accounts.Find(id);
                if (account != null)
                {
                    return new Account()
                    {
                        Id = account.Id,
                        Iban = account.Iban,
                        Currency = account.Currency,
                        Balance = account.Balance,
                        CustomerId = account.CustomerId,
                        Name = account.Name,
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

        public int AddAccount(Account acc)
        {
            try
            {
                var result = _DbContext.Accounts.Add(acc);
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

        public bool UpdateAccount(Account account)
        {
            try
            {
                var AccToUp = _DbContext.Accounts.Find(account.Id);
                AccToUp.Name = account.Name;
                AccToUp.Iban = account.Iban;
                AccToUp.Currency = account.Currency;
                AccToUp.Balance = account.Balance;
                AccToUp.CustomerId = account.CustomerId;
                return _DbContext.SaveChanges() != 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return false;
            }
            
        }

        public bool DelateAccount(int Id)
        {
            try
            {
                var account = _DbContext.Accounts.Find(Id);
                _DbContext.Accounts.Remove(account);
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
