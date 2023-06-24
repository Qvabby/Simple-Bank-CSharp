using Simple_Bank_CSharp.Classes;
using Simple_Bank_CSharp.Commands;
using Simple_Bank_CSharp.Commands.Operations;
using Simple_Bank_CSharp.DataBase;
using Simple_Bank_CSharp.Implementations;
using Simple_Bank_CSharp.Repositories.Models;
using Simple_Bank_CSharp.Services;
using Simple_Bank_CSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Simple_Bank_CSharp
{
    class Program
    {
        static private Logger Logger;
        public class Application
        {


            public readonly IOperationService _operationService;

            public Application(IOperationService operationService)
            {
                _operationService = operationService;
            }

            public static OperationCommand CreateCommand(string command)
            {
                OperationCommand operationCommand;

                if (command == "d" || command == "D")
                {
                    operationCommand = new WithdrawByAccountId();
                }
                else if (command == "c" || command == "C")
                {
                    operationCommand = new DepositByAccountId();
                }
                else
                {
                    throw new ArgumentException(nameof(command));
                }

                operationCommand.HappenedAt =
                    DateTime.ParseExact(Readline("HappenedAt: "), "yyyy/mm/dd", CultureInfo.InvariantCulture);
                operationCommand.AccountId =
                    int.Parse(Readline("AccountId: "));
                operationCommand.Currency =
                    Readline("Currency: ");
                operationCommand.Amount =
                    decimal.Parse(Readline("Amount: "));

                return operationCommand;
            }

            public static OperationCommand CreateCommand(string command, Account account)
            {
                OperationCommand operationCommand;

                if (command == "d" || command == "D")
                {
                    operationCommand = new WithdrawByAccountId();
                }
                else if (command == "c" || command == "C")
                {
                    operationCommand = new DepositByAccountId();
                }
                else
                {
                    throw new ArgumentException(nameof(command));
                }

                operationCommand.HappenedAt =
                    DateTime.ParseExact(Readline("HappenedAt: "), "yyyy/mm/dd", CultureInfo.InvariantCulture);
                operationCommand.AccountId = account.Id;
                operationCommand.Currency =
                    Readline("Currency: ");
                operationCommand.Amount =
                    decimal.Parse(Readline("Amount: "));

                return operationCommand;
            }
        }

        // CONST VARIABLES --------------------------
        const string CSVfolder = @"../../../CSV Files";

        // METHODS ----------------------------------------------
        private static string Readline(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        static Iban GenerateIban()
        {
            // example : GE42TB7756698689794974
            try
            {
                SimpleBankDbContext dbcontext = new SimpleBankDbContext();
                Random rand = new Random();

                StringBuilder Iban = new StringBuilder();
                Iban.Append("GE");
                Iban.Append(Convert.ToString(rand.Next(0, 9)));
                Iban.Append(Convert.ToString(rand.Next(0, 9)));
                Iban.Append("TB");
                for (int i = 0; i <= 15; i++)
                {
                    Iban.Append(Convert.ToString(rand.Next(0, 9)));
                }

                string iban = Iban.ToString();
                foreach (var accs in dbcontext.Accounts)
                {
                    if (accs.Iban == iban)
                    {
                        //recursion
                        GenerateIban();
                    }
                }

                Iban _iban = new Iban(iban);
                return _iban;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return null;
            }
            
        }
        static Account CreateAccount()
        {
            try
            {
                Console.WriteLine("--------Creating Account--------");
                Console.WriteLine("Currency: ");
                string _Currency = Console.ReadLine();
                Console.WriteLine("Name(Optional): ");
                string _Name = Console.ReadLine();
                Iban iban = GenerateIban();
                decimal balance = 0;

                return new Account()
                {
                    Name = _Name,
                    Currency = _Currency,
                    Iban = iban,
                    Balance = balance,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return null;
            }
            
        }
        static Customer Menu()
        {
            try
            {
                Console.WriteLine("1. Login\n2. Register");
                int userchoice = int.Parse(Console.ReadLine());
                UserService userservice = new UserService();

                Console.WriteLine();
                switch (userchoice)
                {
                    case 1:
                        {
                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Password: ");
                            string pass = Console.ReadLine();
                            int customerid = userservice.Login(email, pass);
                            if (customerid == -1)
                            {
                                Console.WriteLine("Incorrect Password or email");
                                return null;
                            }
                            else
                            {
                                Console.WriteLine("You have logged in.");
                                SimpleBankDbContext dbcontext = new SimpleBankDbContext();
                                return dbcontext.Customers.Find(customerid);
                            }
                        }
                    case 2:
                        {
                            //Id,Name,PhoneNumber,Email,Type, password
                            SimpleBankDbContext dbcontext = new SimpleBankDbContext();
                            AccountRepository AR = new AccountRepository();

                            Console.WriteLine("Name: ");
                            string _Name = Console.ReadLine();
                            Console.WriteLine("Email: ");
                            string _Email = Console.ReadLine();
                            Console.WriteLine("Identify Number: ");
                            string _IdentifyNumber = Console.ReadLine();
                            Console.WriteLine("PhoneNumber: ");
                            int _PhoneNumber = int.Parse(Console.ReadLine());
                            Console.WriteLine("Type(0 - Personal / 1 - For Company): ");
                            byte _Type = byte.Parse(Console.ReadLine());
                            Console.WriteLine("Password: ");
                            string _Password = Console.ReadLine();

                            Customer customer = new Customer()
                            {
                                Name = _Name,
                                Email = _Email,
                                IdentityNumber = _IdentifyNumber,
                                PhoneNumber = _PhoneNumber,
                                Type = _Type,
                                Password = _Password,
                            };
                            Account CusAcc = CreateAccount();
                            userservice.Register(customer);
                            CusAcc.CustomerId = customer.Id;
                            AR.AddAccount(CusAcc);
                            return customer;
                        }
                    default:
                        break;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                Console.WriteLine("Try Again.");
                return Menu();
            }
            
        }
        static Account GetAccount(Customer customer)
        {
            try
            {
                SimpleBankDbContext _dbcontext = new SimpleBankDbContext();
                List<Account> CustomerAccs = new List<Account>();
                foreach (var acc in _dbcontext.Accounts)
                {
                    if (acc.CustomerId == customer.Id)
                    {
                        CustomerAccs.Add(acc);
                    }
                }
                Console.WriteLine("Choose your account: ");
                int z = 0;
                foreach (var acc in CustomerAccs)
                {
                    Console.WriteLine($"{z}. {acc.Iban} - {acc.Name}");
                    z++;
                }
                int AccChoice = int.Parse(Console.ReadLine());
                var account = new Account();
                for (int i = 0; i < CustomerAccs.Count; i++)
                {
                    if (i == AccChoice)
                    {
                        account = CustomerAccs[i];
                    }
                }

                return account;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return null;
            }
            
        }

        /// <summary>
        /// third properity string takes either (Account or Operation or Customer otherwise method wont work
        /// </summary>
        /// <param name="path"></param>
        /// <param name="A_C_O">Choose either of - "Account", "Operation", "Cutomer"</param>
        /// <param name="dbContext"></param>
        static void ToCsv(string path, SimpleBankDbContext dbcontext, string A_C_O)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    switch (A_C_O)
                    {
                        case "Account":
                            {
                                foreach (var dbcontextaccount in dbcontext.Accounts)
                                {
                                    sw.WriteLine(Account.ToString(dbcontextaccount));
                                }
                                break;
                            }
                        case "Operation":
                            {
                                foreach (var dbcontextaccount in dbcontext.Operations)
                                {
                                    sw.WriteLine(Operation.ToString(dbcontextaccount));
                                }
                                break;
                            }
                        case "Customer":
                            {
                                foreach (var dbcontextaccount in dbcontext.Customers)
                                {
                                    sw.WriteLine(Customer.ToString(dbcontextaccount));
                                }
                                break;
                            }
                        default:
                            break;
                    }

                }
            }
            catch (Exception e)
            {
                var ex = e;
                Console.WriteLine($"Error Occuired - {ex.Message} ");
                Logger.LogError(ex);
            }
            
        }
        /// <summary>
        /// second properity string takes either (Account or Operation or Customer otherwise method wont work
        /// </summary>
        /// <param name="path"></param>
        /// <param name="A_C_O">Choose either of - "Account", "Operation", "Cutomer"</param>
        /// <returns></returns>
        static dynamic FileReader(string path, string A_C_O)
        {
            try
            {
                switch (A_C_O)
                {
                    case "Account":
                        {
                            var raw = File.ReadAllLines(path)
                                .Skip(1)
                                .Where(s => !String.IsNullOrWhiteSpace(s))
                                .Select(Account.Parse)
                    .ToList();
                            return raw;
                        }
                    case "Operation":
                        {
                            var raw = File.ReadAllLines(path)
                                .Skip(1)
                                .Where(s => !String.IsNullOrWhiteSpace(s))
                                .Select(Operation.Parse)
                                .ToList();
                            return raw;
                        }
                    case "Customer":
                        {
                            var raw = File.ReadAllLines(path)
                                .Skip(1)
                                .Where(s => !String.IsNullOrWhiteSpace(s))
                                .Select(Customer.Parse)
                                .ToList();
                            return raw;
                        }
                    default:
                        break;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occuired - {e.Message} ");
                Logger.LogError(e);
                return null;
            }
            
        }
        //----------------------Main-----------------------------------
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World"); - c#
            //print("Hello World") - python
            //Console.log("Hello World") - js
            OperationService op = new OperationService(new AccountRepository(), new OperationRepository(), new DateTimeProvider());
            Application app = new Application(op);

            AccountRepository acc = new AccountRepository();
            CustomerRepository customer = new CustomerRepository();
            SimpleBankDbContext dbcontext = new SimpleBankDbContext();

            string path = CSVfolder + @"/DbAccounts.csv";
            string path2 = CSVfolder + @"/DbCustomers.csv";
            string path3 = CSVfolder + @"/DbOperations.csv";

            ToCsv(path, dbcontext, "Account");
            ToCsv(path2, dbcontext, "Customer");
            ToCsv(path3, dbcontext, "Operation");

            var AccountsRawData = FileReader(path, "Account");
            var CustomersRawData = FileReader(path2, "Customer");
            var Operations = FileReader(path3, "Operation");

            var Loggedcustomer = Menu();
            var ChoosenAcc = GetAccount(Loggedcustomer);
            while (true)
            {
                try
                {
                    Console.WriteLine("For Debit Operation enter - [c] / [C]");
                    Console.WriteLine("For Withdraw Operation enter - [d] / [D]");
                    var command = Console.ReadLine();

                    switch (Application.CreateCommand(command, ChoosenAcc))
                    {
                        case WithdrawByAccountId CreditByAccountId:
                            {

                                app._operationService.Withdraw(CreditByAccountId);
                                break;
                            }

                        case DepositByAccountId debitByAccountId:
                            {
                                app._operationService.Deposit(debitByAccountId);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Logger.LogError(e);
                }
            }

            try
            {
                dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                Console.WriteLine(e.Message);
            }
        }
    }
}