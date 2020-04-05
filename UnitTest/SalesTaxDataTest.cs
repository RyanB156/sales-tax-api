using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxCalculator;

namespace UnitTest
{
    [TestClass]
    public class SalesTaxDataTest
    {
        private SalesTaxData data;

        public SalesTaxDataTest()
        {
            data = new SalesTaxData();
        }

        [TestMethod]
        public void ReturnValueTest1()
        {
            Assert.AreEqual(data.GetTaxAmount("Alamance County"), 6.75);
        }

        [TestMethod]
        public void ReturnValueTest2()
        {
            Assert.AreEqual(data.GetTaxAmount("Buncombe County"), 7.00);
        }

        [TestMethod]
        public void ReturnValueTest3()
        {
            Assert.AreEqual(data.GetTaxAmount("countyname"), -1);
        }

        [TestMethod]
        public void ReturnValueTest4()
        {
            Assert.AreEqual(data.GetTaxAmount(null), -1);
        }

        [TestMethod]
        public void ReturnValueTest5()
        {
            Assert.AreEqual(data.GetTaxAmount(""), -1);
        }
    }
}
