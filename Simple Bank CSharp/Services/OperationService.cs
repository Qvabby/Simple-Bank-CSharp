using Simple_Bank_CSharp.Commands.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Repositories;
using Simple_Bank_CSharp.Utilities;
using Simple_Bank_CSharp.Repositories.Models;
using Simple_Bank_CSharp.Exceptions;

namespace Simple_Bank_CSharp.Services
{
    class OperationService : IOperationService
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IOperationRepository _operationRepository;
        private readonly IDateTimeProvider _dateTime;

        public OperationService(IAccountRepository accountRepository, IOperationRepository operationRepository, IDateTimeProvider dateTime)
        {
            _accountRepository = accountRepository;
            _operationRepository = operationRepository;
            _dateTime = dateTime;
        }

        /// <summary>
        /// Money Deposit Operation (ფულის ჩარიცხვის ოპერაცია)
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public long Deposit(DepositByAccountId command)
        {
            var account = _accountRepository.GetAccountById(command.AccountId);
            var DepositAmount = command.Amount;

            account.Balance += DepositAmount;
            _accountRepository.UpdateAccount(account);

            return _operationRepository.AddOperation(new Operation()
            {
                Type = OperationType.Deposit,
                Currency = command.Currency,
                Amount = command.Amount,
                AccountId = account.Id,
                CustomerId = account.CustomerId,
                HappenedAt = command.HappenedAt,
                CreatedAt = _dateTime.Now,
            });
        }
        /// <summary>
        /// Money Withdraw Operation (ფულის ჩამოჭრის ოპერაცია)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public long Withdraw(WithdrawByAccountId command)
        {
            var account = _accountRepository.GetAccountById(command.AccountId);
            var OperationAmount = command.Amount;

            if (account.Balance < OperationAmount)
            {
                throw new InsufficientFundsException(account.Id);
            }

            account.Balance -= command.Amount;
            _accountRepository.UpdateAccount(account);

            return _operationRepository.AddOperation(new Operation()
            {
                Type = OperationType.Withdraw,
                Currency = command.Currency,
                Amount = command.Amount,
                AccountId = account.Id,
                CustomerId = account.CustomerId,
                HappenedAt = command.HappenedAt,
                CreatedAt = _dateTime.Now,
            });
        }



    }
}
