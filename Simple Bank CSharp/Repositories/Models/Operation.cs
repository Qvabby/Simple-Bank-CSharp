using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Repositories.Models
{
    class Operation
    {
        [Key]
       // Id, Type, Currency, Amount, AccountId, CustomerId, HappenedAt, CreatedAt
        public long Id { get; set; }
        public OperationType Type { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public DateTime HappenedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public static Operation Parse(string text)
        {
            var RawData = text.Split(',');

            if (RawData.Length != 7 && RawData.Length != 8)
            {
                throw new FormatException("Invalid Format of Operation Class");
            }

            OperationType type = (OperationType)Enum.Parse(typeof(OperationType), RawData[1]);
            return new Operation()
            {
                Type = (OperationType)type,
                Currency = RawData[2],
                Amount = decimal.Parse(RawData[3]),
                AccountId = int.Parse(RawData[4]),
                CustomerId = int.Parse(RawData[5]),
                HappenedAt = DateTime.Parse(RawData[6]),
                CreatedAt = DateTime.Parse(RawData[7])
            };
        }
        public static string ToString(Operation operation)
        {
            return $"{operation.Id},{operation.Type},{operation.Currency},{operation.Amount},{operation.AccountId},{operation.CustomerId},{operation.HappenedAt},{operation.CreatedAt}";
        }
    }
}
