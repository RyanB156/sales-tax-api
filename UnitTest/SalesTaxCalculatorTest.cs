using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxCalculator;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesTaxCalculator.Json;
using System.Text;

namespace UnitTest
{

    /// <summary>
    /// Tests the entire system from the input URL to the final output.
    /// </summary>
    [TestClass]
    public class SalesTaxCalculatorTest
    {

        // Test results for Alamance County with a price of $10.00
        [TestMethod]
        public async Task CalculationTest1()
        {
            string url = "http://localhost:8080/salestax/";
            using (var client = new HttpClient())
            {
                InputData inputData = new InputData()
                {
                    County = "Alamance County",
                    Price = 10.00m
                };

                SaleResult saleResult = new SaleResult()
                {
                    County = "alamance county",
                    SalePrice = 10.00m,
                    TaxRate = 6.75m,
                    SaleTotal = 10.675m
                };

                var json = JsonConvert.SerializeObject(inputData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

                SaleResult queryResult = JsonConvert.DeserializeObject<SaleResult>(result);

                Assert.AreEqual(saleResult.County, queryResult.County);
                Assert.AreEqual(saleResult.SalePrice, queryResult.SalePrice);
                Assert.AreEqual(saleResult.TaxRate, queryResult.TaxRate);
                Assert.AreEqual(Math.Round(saleResult.SaleTotal, 3), Math.Round(queryResult.SaleTotal, 3));

            }

        }

        // Test results for Alamance County with a price of $10.00
        [TestMethod]
        public async Task CalculationTest2()
        {

            // TODO: Test the entire system from start to finish. Give a URL and check the total price returned...

            using (var client = new HttpClient())
            {
                string countyName = "Alamance County";
                double price = 10.00;

                SaleResult saleResult = new SaleResult()
                {
                    County = "alamance county",
                    SalePrice = 10.00m,
                    TaxRate = 6.75m,
                    SaleTotal = 10.675m
                };

                string url = $"http://localhost:8080/salestax/?county={countyName}&price={price}";

                var response = await client.GetAsync(url);

                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

                SaleResult queryResult = JsonConvert.DeserializeObject<SaleResult>(result);

                Assert.AreEqual(saleResult.County, queryResult.County);
                Assert.AreEqual(saleResult.SalePrice, queryResult.SalePrice);
                Assert.AreEqual(saleResult.TaxRate, queryResult.TaxRate);
                Assert.AreEqual(Math.Round(saleResult.SaleTotal, 3), Math.Round(queryResult.SaleTotal, 3));

            }

        }


        // Test results for Lincoln County with a price of $10.00
        [TestMethod]
        public async Task CalculationTest3()
        {
            string url = "http://localhost:8080/salestax/";
            using (var client = new HttpClient())
            {
                InputData inputData = new InputData()
                {
                    County = "lincoln_county",
                    Price = 10.00m
                };

                SaleResult saleResult = new SaleResult()
                {
                    County = "lincoln county",
                    SalePrice = 10.00m,
                    TaxRate = 7.00m,
                    SaleTotal = 10.700m
                };

                var json = JsonConvert.SerializeObject(inputData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

                SaleResult queryResult = JsonConvert.DeserializeObject<SaleResult>(result);

                Assert.AreEqual(saleResult.County, queryResult.County);
                Assert.AreEqual(saleResult.SalePrice, queryResult.SalePrice);
                Assert.AreEqual(saleResult.TaxRate, queryResult.TaxRate);
                Assert.AreEqual(Math.Round(saleResult.SaleTotal, 3), Math.Round(queryResult.SaleTotal, 3));

            }

        }

        // Test results for Lincoln County with a price of $10.00
        [TestMethod]
        public async Task CalculationTest4()
        {
            using (var client = new HttpClient())
            {
                string countyName = "lincoln_county";
                double price = 10.00;

                SaleResult saleResult = new SaleResult()
                {
                    County = "lincoln county",
                    SalePrice = 10.00m,
                    TaxRate = 7.00m,
                    SaleTotal = 10.700m
                };

                string url = $"http://localhost:8080/salestax/?county={countyName}&price={price}";

                var response = await client.GetAsync(url);

                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

                SaleResult queryResult = JsonConvert.DeserializeObject<SaleResult>(result);

                Assert.AreEqual(saleResult.County, queryResult.County);
                Assert.AreEqual(saleResult.SalePrice, queryResult.SalePrice);
                Assert.AreEqual(saleResult.TaxRate, queryResult.TaxRate);
                Assert.AreEqual(Math.Round(saleResult.SaleTotal, 3), Math.Round(queryResult.SaleTotal, 3));

            }

        }

        // Test results for Scotland County with a price of $10.00
        [TestMethod]
        public async Task CalculationTest5()
        {
            string url = "http://localhost:8080/salestax/";
            using (var client = new HttpClient())
            {
                InputData inputData = new InputData()
                {
                    County = "SCOTLAND_COUNTY",
                    Price = 10.00m
                };

                SaleResult saleResult = new SaleResult()
                {
                    County = "scotland county",
                    SalePrice = 10.00m,
                    TaxRate = 6.75m,
                    SaleTotal = 10.675m
                };

                var json = JsonConvert.SerializeObject(inputData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

                SaleResult queryResult = JsonConvert.DeserializeObject<SaleResult>(result);

                Assert.AreEqual(saleResult.County, queryResult.County);
                Assert.AreEqual(saleResult.SalePrice, queryResult.SalePrice);
                Assert.AreEqual(saleResult.TaxRate, queryResult.TaxRate);
                Assert.AreEqual(Math.Round(saleResult.SaleTotal, 3), Math.Round(queryResult.SaleTotal, 3));

            }

        }

        // Test results for Scotland County with a price of $10.00
        [TestMethod]
        public async Task CalculationTest6()
        {
            using (var client = new HttpClient())
            {
                string countyName = "SCOTLAND_COUNTY";
                double price = 10.00;

                SaleResult saleResult = new SaleResult()
                {
                    County = "scotland county",
                    SalePrice = 10.00m,
                    TaxRate = 6.75m,
                    SaleTotal = 10.675m
                };

                string url = $"http://localhost:8080/salestax/?county={countyName}&price={price}";

                var response = await client.GetAsync(url);

                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

                SaleResult queryResult = JsonConvert.DeserializeObject<SaleResult>(result);

                Assert.AreEqual(saleResult.County, queryResult.County);
                Assert.AreEqual(saleResult.SalePrice, queryResult.SalePrice);
                Assert.AreEqual(saleResult.TaxRate, queryResult.TaxRate);
                Assert.AreEqual(Math.Round(saleResult.SaleTotal, 3), Math.Round(queryResult.SaleTotal, 3));

            }

        }

    }
}
