using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bank_CSharp.Utilities
{
    class Logger : ILogger
    {

        public void LogError(Exception error)
        {
            const string path = @"../../../CSV Files/ErrorLog.txt";
            
            using (StreamWriter tw = new StreamWriter(path, true))
            {
                tw.WriteLine(error.Message);
                tw.Close();
            }

        }
    }
}
