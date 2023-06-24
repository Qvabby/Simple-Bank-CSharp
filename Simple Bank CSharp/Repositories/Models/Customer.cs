using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Classes
{
    class Customer
    {
        //Id,Name,IdentityNumber,PhoneNumber,Email,Type, password
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte Type { get; set; }
        public string Password { get; set; }

        public static Customer Parse(string text)
        {
            var RawData = text.Split(',');

            if (RawData.Length != 6 && RawData.Length != 7)
            {
                throw new FormatException("Invalid Format of Customer Class");
            }
            return new Customer()
            {
                Name = RawData[1],
                IdentityNumber = RawData[2],
                PhoneNumber = int.Parse(RawData[3]),
                Email = RawData[4],
                Type = byte.Parse(RawData[5]),
                Password = RawData[6]
            };
        }

        public static string ToString(Customer customer)
        {
            return $"{customer.Id},{customer.Name},{customer.IdentityNumber},{customer.PhoneNumber},{customer.Email},{customer.Type}, {customer.Password}";
        }

    }
}
