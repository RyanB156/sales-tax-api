using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using SalesTaxCalculator.Json;
using Newtonsoft.Json;
using System.Text;

namespace UnitTest
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [TestClass]
    public class PostTests
    {
        // -- POST Unit Tests


        // Test parsing of valid input data.
        [TestMethod]
        public async Task PostTest1()
        {

            InputData[] inputData =
            {
                new InputData() { County = "Alamance County", Price = 10.00 },
                new InputData() { County = "moore_county", Price = 1 },
                new InputData() { County = "LENOIR COUNTY", Price = 10.00 },
                new InputData() { County = "robeson county", Price = 10.00 },
            };

            string url = "http://localhost:8080/salestax/";
            using (var client = new HttpClient())
            {

                foreach (InputData data in inputData)
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }
            }
        }

        // Test parsing of invalid input data.
        [TestMethod]
        public async Task PostTest2()
        {

            InputData[] inputData =
            {
                new InputData() { County = "Alama County", Price = 10.00 },
                new InputData() { County = "moore___county", Price = 1 },
                new InputData() { County = "LENOIR COU5NTY", Price = 10.00 },
                new InputData() { County = "robeson", Price = 10.00 },
            };

            string url = "http://localhost:8080/salestax/";
            using (var client = new HttpClient())
            {

                foreach (InputData data in inputData)
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }
            }
        }

        [TestMethod]
        public async Task PostTest3()
        {

            string url = "http://localhost:8080/salestax/";
            using (var client = new HttpClient())
            {

                Person p = new Person() { Name = "Ryan", Age = 22 };

                var json = JsonConvert.SerializeObject(p);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
            }
        }
    }
}
