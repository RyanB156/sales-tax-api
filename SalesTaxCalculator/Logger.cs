using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator
{
    class Logger
    {

        private enum LogType { Debug, Error, Info }

        private void Log(object o, string message)
        {
            using (StreamWriter sw = new StreamWriter(@"..\..\Logs\log.txt", true))
            {
                sw.WriteLine($"{DateTime.Now} [{message}] : \"{o.ToString()}\"");
            }
        }

        public void Info(object o)
        {
            Log(o, "Info");
        }

        public void Debug(object o)
        {
            Log(o, "Debug");
        }

        public void Error(object o)
        {
            Log(o, "Error");
        }
    }

    
}
