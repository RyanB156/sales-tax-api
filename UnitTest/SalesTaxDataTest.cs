using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxCalculator;

namespace UnitTest
{

    /// <summary>
    /// Unit testing of the sales tax data lookup procedure.
    /// </summary>
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
            Assert.AreEqual(6.75m, data.GetTaxAmount("alamance county"));
        }

        [TestMethod]
        public void ReturnValueTest2()
        {
            Assert.AreEqual(7.00m, data.GetTaxAmount("buncombe county"));
        }

        [TestMethod]
        public void ReturnValueTest3()
        {
            Assert.AreEqual(-1m, data.GetTaxAmount("countyname"));
        }

        [TestMethod]
        public void ReturnValueTest4()
        {
            Assert.AreEqual(-1m, data.GetTaxAmount(null));
        }

        [TestMethod]
        public void ReturnValueTest5()
        {
            Assert.AreEqual(-1m, data.GetTaxAmount(""));
        }
    }
}
