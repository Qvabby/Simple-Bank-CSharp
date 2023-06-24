using System;
using System.Linq;
using Simple_Bank_CSharp.Exceptions;

namespace Simple_Bank_CSharp.Classes
{
    public sealed class Iban
    {
        public const int Length = 22;
        public string Value { get;}
        public string CountryCode { get; }
        public Iban(string value)
        {
            Validate(value);

            Value = value;

            CountryCode = new string(new[] { value[0], value[1] });
        }

        public static implicit operator string(Iban iban) => iban.Value;
        public static implicit operator Iban(string value) => new Iban(value);

        public override string ToString()
        {
            return Value;
        }
        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new IbanEmptyException(value);
            }

            if (value.Length != Length || !value.Take(2).All(char.IsLetter))
            {
                throw new IbanInvalidStructureException(value);
            }
        }

    }
}