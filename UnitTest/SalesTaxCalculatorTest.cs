using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxCalculator;

namespace UnitTest
{
    [TestClass]
    public class SalesTaxCalculatorTest
    {
        private SalesTaxData data;

        public SalesTaxCalculatorTest()
        {
            data = new SalesTaxData();
        }

        [TestMethod]
        public void ReturnValueTest1()
        {
            Assert.AreEqual(data.GetTaxAmount("Alamance County"), 6.75);
        }

    }
}
