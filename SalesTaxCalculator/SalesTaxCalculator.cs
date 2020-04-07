using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace SalesTaxCalculator
{
    public class SalesTaxCalculator
    {
        private HttpListener server;
        private string prefix;
        private byte[] bytes;
        private string data;

        private Logger logger;

        SalesTaxData taxData;
        Json.JsonResponseBuilder builder;

        public SalesTaxCalculator()
        {
            logger = new Logger();
            logger.Clear();

            // Set the server to listen to HTTP requests at the endpoint "salestax" on port 8080.
            prefix = "http://localhost:8080/salestax/";
            server = new HttpListener();

            bytes = new byte[256];
            data = "";

            taxData = new SalesTaxData();
            builder = new Json.JsonResponseBuilder();
        }

        // TODO: Handle all possible failure conditions and send an error message detailing what went wrong...
        public Status ParseRequest(HttpListenerRequest request, out string countyName, out double price)
        {
            countyName = null;
            price = -1;

            if (request.HttpMethod == "GET")
            {
                logger.Info("Got GET request");
                logger.Debug($"Received RawURL: {request.RawUrl}");

                var requestQuery = request.QueryString;

                if (requestQuery.Contains("county") && requestQuery.Contains("price"))
                {
                    foreach (var pair in requestQuery)
                    {
                        logger.Debug($"Query: {pair}, value: {requestQuery[(string)pair]}");
                    }

                    // Attempt to parse the county name and price from the URI.
                    countyName = request.QueryString["county"].Replace('_', ' ').ToLower();
                    if (!Double.TryParse(requestQuery["price"], out price))
                        return Status.Failure("Invalid price");

                    logger.Debug($"Got county: {countyName} and price: {price} from GET request");

                }
                else
                {
                    return Status.Failure("Expected 'county' and 'price' in the URI");
                }

                return Status.Success();
            }
            else if (request.HttpMethod == "POST")
            {

                if (!request.HasEntityBody || !request.ContentType.Split(';').Contains("application/json"))
                    return Status.Failure("Expected a JSON body");

                request.InputStream.Read(bytes, 0, bytes.Length);
                string body = Encoding.ASCII.GetString(bytes).Trim(new char[] { '\0' });

                logger.Debug($"Got POST request with body {body}");

                Json.InputData inputData = JsonConvert.DeserializeObject<Json.InputData>(body);

                if (inputData == null)
                    return Status.Failure("Expected a JSON body");

                if (inputData.County == null || inputData.Price <= 0)
                    return Status.Failure("Invalid county or price in JSON body");

                countyName = inputData.County.Replace('_', ' ').ToLower();
                price = inputData.Price;

                logger.Debug($"JSON Deserializes into County: {countyName}, Price: {price}");


                return Status.Success();
            }
            else
            {
                return Status.Failure("Unsopported HTTP method");
            }
            
        }

        public double QueryDatabase(string countyName, double price)
        {
            return taxData.GetTaxAmount(countyName);
        }

        public Json.SaleResult CalculateResult(string countyName, double price, double taxRate)
        {
            return new Json.SaleResult()
            {
                County = countyName,
                SalePrice = price,
                TaxRate = taxRate,
                SaleTotal = price * (1 + taxRate / 100.0)
            };
        }

        public void Start()
        {
            try
            {
                server.Prefixes.Add(prefix);
                server.Start();
                logger.Info("Starting the server");

                Status calculationStatus;

                while (true)
                {
                    bytes = new byte[256];

                    logger.Info("Waiting for a connection");
                    HttpListenerContext context = server.GetContext();
                    logger.Info("Connected");
                    HttpListenerRequest request = context.Request;
                    var requestQuery = request.QueryString;

                    HttpListenerResponse response = context.Response;

                    // Generate reponse message
                    string responseString = "";
                    response.ContentType = "application/json";

                    Status parseStatus = ParseRequest(request, out string countyName, out double price);

                    if (parseStatus.isSuccess)
                    {

                        logger.Info("Successful Parse");

                        logger.Debug($"Successful query with county={countyName} and price={price}");

                        double taxRate = QueryDatabase(countyName, price);
                        if (taxRate != -1)
                        {
                            responseString = builder.SuccessData(CalculateResult(countyName, price, taxRate));
                            response.StatusCode = 200;
                            calculationStatus = Status.Success();
                        }
                        else
                        {
                            calculationStatus = Status.Failure($"Unable to find the tax rate for county '{countyName}'");
                        }
                    }
                    else
                    {
                        calculationStatus = parseStatus;
                    }

                    if (!calculationStatus.isSuccess)
                    {
                        logger.Error("Bad HTTP request");
                        responseString = builder.ErrorMessage(calculationStatus);
                        response.StatusCode = (request.HttpMethod == "GET") ? 404 : 400;
                    }


                    // Send HTTP response with the set headers and body.
                    logger.Debug($"Sending {responseString}");
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    output.Close();
                    logger.Break("--End of Connection--");

                }
            }
            catch (HttpListenerException e)
            {
                logger.Error(e);
            }
            finally
            {
                server.Stop();
            }
            
        }
    }
}
