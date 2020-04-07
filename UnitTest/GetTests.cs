using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;

namespace UnitTest
{
    [TestClass]
    public class GetTests
    {
        // Test a list of good GET query arguments to ensure that they parse correctly.
        [TestMethod]
        public async Task GetTest1()
        {
            string[,] queryArguments = new string[,]
            {
                { "Alamance County", "5" },
                { "moore_COUNTY" , "10.00"},
                { "pamlico county", "123" },
                { "ORANGE_COUNTY", "0.001" }
            };

            using (var client = new HttpClient())
            {
                string url;
                for (int i = 0; i < queryArguments.GetLength(0); i++)
                {
                    url = $"http://localhost:8080/salestax/?county={queryArguments[i, 0]}&price={queryArguments[i, 1]}";
                    var response = await client.GetAsync(url);

                    Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }
            }
        }

        // Test a list of bad GET query arguments to ensure that they parse correctly.
        [TestMethod]
        public async Task GetTest2()
        {
            string[,] queryArguments = new string[,]
            {
                { "Alaman County", "5" },
                { "moore" , "10.00"},
                { "pamlico cou  nty", "123" },
                { "ORANGE777COUNTY", "0.001" },
                { "Robeson County", "" },
                { "Lincoln County", "a" },
                { "Lenoir County", "---" }
            };

            using (var client = new HttpClient())
            {
                string url;
                for (int i = 0; i < queryArguments.GetLength(0); i++)
                {
                    url = $"http://localhost:8080/salestax/?county={queryArguments[i, 0]}&price={queryArguments[i, 1]}";
                    var response = await client.GetAsync(url);

                    Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);

                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }
            }
        }

        // Test valid GET request with spaces between the words
        [TestMethod]
        public async Task GetTest3()
        {
            string url = "http://localhost:8080/salestax/?county=new hanover county&price=10.00";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

            }
        }

        [TestMethod]
        public async Task GetTest4()
        {
            string url = "http://localhost:8080/salestax/";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

            }
        }

        [TestMethod]
        public async Task GetTest5()
        {
            string url = "http://localhost:8080/salestax/county=macon county&price=10.00";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

            }
        }

        [TestMethod]
        public async Task GetTest6()
        {
            string url = "http://localhost:8080/salestax/?name=abc&day=Monday";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

            }
        }
    }
}
