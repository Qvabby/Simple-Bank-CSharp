using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Classes
{
    class Account
    {
        //Id,Iban,Currency,Balance,CustomerId,Name
        [Key]

        public int Id { get; set; }
        public string Iban { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public string? Name { get; set; }

        public static Account Parse(string text)
        {
            var RawData = text.Split(',');

            if (RawData.Length != 5 && RawData.Length != 6)
            {
                throw new FormatException("Invalid Format of Account Class");
            }
            return new Account()
            {
                Iban = RawData[1],
                Currency = RawData[2],
                Balance = decimal.Parse(RawData[3]),
                CustomerId = int.Parse(RawData[4]),
                Name = RawData[5]
            };
        }


        public static string ToString(Account account)
        {
            return $"{account.Id},{account.Iban},{account.Currency},{account.Balance},{account.CustomerId},{account.Name}";
        }

    }
}
