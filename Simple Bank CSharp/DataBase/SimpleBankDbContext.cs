using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple_Bank_CSharp.Classes;
using Simple_Bank_CSharp.Repositories.Models;

namespace Simple_Bank_CSharp.DataBase
{
    class SimpleBankDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Operation> Operations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-PS7OKKF;Database=SimpleBankDB;Trusted_Connection=True;");
        }
    }
}
