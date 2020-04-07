using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Json
{
    public class SaleResult
    {
        public string County { get; set; }
        public double SalePrice { get; set; }
        public double TaxRate { get; set; }
        public double SaleTotal { get; set; }
    }
}
