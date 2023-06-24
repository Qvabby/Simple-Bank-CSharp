using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Repositories;
using Simple_Bank_CSharp.Repositories.Models;
using Simple_Bank_CSharp.DataBase;
using Simple_Bank_CSharp.Utilities;

namespace Simple_Bank_CSharp.Implementations
{
    class OperationRepository : IOperationRepository
    {

        private readonly SimpleBankDbContext _DbContext;
        private Logger Logger;
        public OperationRepository()
        {
            _DbContext = new SimpleBankDbContext();
        }

        public long AddOperation(Operation operation)
        {
            try
            {
                var result = _DbContext.Operations.Add(operation);
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

        public bool DeleteOperation(int Id)
        {
            try
            {
                var operation = _DbContext.Operations.Find(Id);
                _DbContext.Operations.Remove(operation);
                return _DbContext.SaveChanges() != 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return false;
            }
            
        }

        public Operation GetOperationById(long id)
        {
            try
            {
                var operation = _DbContext.Operations.Find(id);
                if (operation != null)
                {
                    return new Operation()
                    {
                        Id = operation.Id,
                        Type = operation.Type,
                        Currency = operation.Currency,
                        Amount = operation.Amount,
                        AccountId = operation.AccountId,
                        CustomerId = operation.CustomerId,
                        HappenedAt = operation.HappenedAt,
                        CreatedAt = operation.CreatedAt,
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

        public bool UpdateOperation(Operation operation)
        {
            try
            {
                // Id, Type, Currency, Amount, AccountId, CustomerId, HappenedAt, CreatedAt
                var CustToUp = _DbContext.Operations.Find(operation.Id);
                CustToUp.Type = operation.Type;
                CustToUp.Currency = operation.Currency;
                CustToUp.Amount = operation.Amount;
                CustToUp.AccountId = operation.AccountId;
                CustToUp.CustomerId = operation.CustomerId;
                CustToUp.HappenedAt = operation.HappenedAt;
                CustToUp.CreatedAt = operation.CreatedAt;
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
