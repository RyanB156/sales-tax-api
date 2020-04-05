﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator
{
    /// <summary>
    /// A simple logging class.
    /// </summary>
    class Logger
    {
        private readonly string path = @"..\..\Logs\log.txt";
        private enum LogType { Debug, Error, Info }


        /// <summary>
        /// Write messages in the log file with the current date and time, a message type, and the information to be written
        /// </summary>
        /// <param name="o">The object to be written in the log file</param>
        /// <param name="message">The log message type [Info, Debug, Error]</param>
        private void Log(object o, string message)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{DateTime.Now} [{message + "]", -6} : \"{o.ToString()}\"");
            }
        }

        /// <summary>
        /// Write basic information in the log file
        /// </summary>
        /// <param name="o">The object to be written in the file</param>
        public void Info(object o)
        {
            Log(o, "Info");
        }

        /// <summary>
        /// Write information containing variables in the log file
        /// </summary>
        /// <param name="o">The object to be written in the file</param>
        public void Debug(object o)
        {
            Log(o, "Debug");
        }

        /// <summary>
        /// Write exception messages in the log file
        /// </summary>
        /// <param name="o">The object to be written in the file</param>
        public void Error(object o)
        {
            Log(o, "Error");
        }

        /// <summary>
        /// Clear all contents of the log file
        /// </summary>
        public void Clear()
        {
            File.WriteAllText(path, String.Empty);
        }
    }

    
}
