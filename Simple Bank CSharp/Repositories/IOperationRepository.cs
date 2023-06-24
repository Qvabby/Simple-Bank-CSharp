using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Bank_CSharp.Repositories.Models;

namespace Simple_Bank_CSharp.Repositories
{
    interface IOperationRepository
    {
        public Operation GetOperationById(long id);

        public long AddOperation(Operation operation);

        public bool UpdateOperation(Operation operation);

        public bool DeleteOperation(int Id);
    }
}
