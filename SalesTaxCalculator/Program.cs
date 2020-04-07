using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator
{
    class Program
    {
        /*
         * TODO:
         *  Get GET requests working
         *  Get POST requests working
         * 
         * 
         *  Testing
         *  Remove logging statements from all classes
         */ 


        static void Main(string[] args)
        {
            SalesTaxCalculator calculator = new SalesTaxCalculator();
            calculator.Start();
        }
    }
}
