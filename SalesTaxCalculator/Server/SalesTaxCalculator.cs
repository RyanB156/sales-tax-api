using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using SalesTaxCalculator.Json;

namespace SalesTaxCalculator
{

    /// <summary>
    /// Creates a web server to listen for HTTP requests to the "salestax" endpoint.
    /// Parses the request, fetches the tax rate for the specified county, and returns the total price after tax is applied.
    /// </summary>
    public class SalesTaxCalculator
    {
        private HttpListener server;
        private readonly string prefix;
        private byte[] bytes;

        private Logger logger;

        private SalesTaxData taxData;
        private JsonResponseBuilder builder;

        public SalesTaxCalculator()
        {
            logger = new Logger();
            logger.Clear();

            // Set the server to listen to HTTP requests at the endpoint "salestax" on port 8080.
            prefix = "http://localhost:8080/salestax/";
            server = new HttpListener();

            bytes = new byte[256];

            taxData = new SalesTaxData();
            builder = new JsonResponseBuilder();
        }

        /// <summary>
        /// Parse the HTTP request and attempt to initialize the variables "countyName" and "price" with the data.
        /// </summary>
        /// <param name="request">The HTTP request to parse.</param>
        /// <param name="countyName">The county name variable to initialize.</param>
        /// <param name="price">The price variable to initialize.</param>
        /// <returns>A Status type representing either a success or a failure with an error message.</returns>
        public Status ParseRequest(HttpListenerRequest request, out string countyName, out decimal price)
        {
            countyName = null;
            price = -1;

            if (request.HttpMethod == "GET")
            {
                logger.Info($"GET request with raw URL {request.RawUrl}");

                var requestQuery = request.QueryString;

                if (requestQuery.Contains("county") && requestQuery.Contains("price"))
                {
                    // Attempt to parse the county name and price from the URI.
                    countyName = request.QueryString["county"].Replace('_', ' ').ToLower();
                    if (!Decimal.TryParse(requestQuery["price"], out price))
                        return Status.Failure("PriceNullError");
                    if (price <= 0)
                        return Status.Failure("PriceInvalidError");
                }
                else
                {
                    return Status.Failure("QueryError");
                }

                return Status.Success();
            }
            else if (request.HttpMethod == "POST")
            {

                // Ensure that the body is in JSON format.
                if (!request.HasEntityBody || !request.ContentType.Split(';').Contains("application/json"))
                    return Status.Failure("JsonContentError");

                // Read the request body and remove trailing characters.
                request.InputStream.Read(bytes, 0, bytes.Length);
                string body = Encoding.ASCII.GetString(bytes).Trim(new char[] { '\0' });

                logger.Debug($"Got POST request with body {body}");

                InputData inputData = JsonConvert.DeserializeObject<InputData>(body);

                if (inputData == null)
                    return Status.Failure("JsonBodyError");

                if (inputData.County == null)
                    return Status.Failure("JsonCountyError");

                if (inputData.Price <= 0)
                    return Status.Failure("PriceInvalidError");

                countyName = inputData.County.Replace('_', ' ').ToLower();
                price = inputData.Price;
                return Status.Success();
            }
            else
            {
                return Status.Failure("UnsupportedMethodError");
            }
            
        }

        /// <summary>
        /// Lookup the tax rate for the specified county in the database.
        /// </summary>
        /// <param name="countyName">The county for which to look up the tax rate.</param>
        /// <returns>The tax rate if the lookup was successful, -1 otherwise.</returns>
        public decimal QueryDatabase(string countyName)
        {
            return taxData.GetTaxAmount(countyName);
        }

        /// <summary>
        /// Calculates the total price after sales tax and creates a SaleResult object to send to the client.
        /// </summary>
        /// <param name="countyName">The name of the county in the request.</param>
        /// <param name="price">The sale price in the request.</param>
        /// <param name="taxRate">The tax rate for the specified county.</param>
        /// <returns>A SaleResult object that contains the response data for the client.</returns>
        public SaleResult CalculateResult(string countyName, decimal price, decimal taxRate)
        {
            return new SaleResult()
            {
                County = countyName,
                SalePrice = price,
                TaxRate = taxRate,
                SaleTotal = price * (1 + taxRate / 100.0m)
            };
        }

        /// <summary>
        /// Send a message to the client.
        /// </summary>
        /// <param name="response">The HttpListenerResponse to encode information in.</param>
        /// <param name="responseString">The message to send.</param>
        private void Send(HttpListenerResponse response, string responseString)
        {
            // Send HTTP response with the set headers and body.
            logger.Debug($"Sending {responseString}");
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }

        /// <summary>
        /// Inform the client that the request was successful and send a JSON response.
        /// </summary>
        /// <param name="response">The HttpListenerResponse to encode information in.</param>
        /// <param name="responseString">The message to send.</param>
        private void SendSuccess(HttpListenerResponse response, string responseString)
        {
            response.StatusCode = 200;
            Send(response, responseString);
        }

        /// <summary>
        /// Inform the client that the request failed and send a JSON error message.
        /// </summary>
        /// <param name="response">The HttpListenerResponse to encode information in.</param>
        /// <param name="failureStatus">A Status object containing the error message.</param>
        /// <param name="requestMethod">The HTTP request method used in the request.</param>
        private void SendFailure(HttpListenerResponse response, Status failureStatus, string requestMethod)
        {
            // GET requests return "Not Found" on failed requests.
            // POST and all other requests return "Bad Request" on failed requests.
            int statusCode = (requestMethod.Equals("GET")) ? 404 : 400;
            response.StatusCode = statusCode;
            string responseString = builder.ErrorMessage(failureStatus);
            Send(response, responseString);
        }


        /// <summary>
        /// Start the server and begin listening for requests.
        /// </summary>
        public void Start()
        {
            try
            {
                server.Prefixes.Add(prefix);
                server.Start();
                logger.Info("Starting the server");

                while (true)
                {
                    bytes = new byte[256];

                    logger.Info("Waiting for a connection");
                    HttpListenerContext context = server.GetContext();

                    // Retrieve request and response objects from the HTTP request.
                    HttpListenerRequest request = context.Request;
                    logger.Info($"Connected with URL: {request.RawUrl}");
                    var requestQuery = request.QueryString;
                    HttpListenerResponse response = context.Response;

                    // The response will contain a JSON body.
                    response.ContentType = "application/json";

                    if (!request.Url.LocalPath.Equals("/salestax/"))
                    {
                        SendFailure(response, Status.Failure("LocalPathError"), request.HttpMethod);
                    }
                    else
                    {
                        // Parse the request to get the county name and sale price.
                        Status parseStatus = ParseRequest(request, out string countyName, out decimal price);

                        if (parseStatus.isSuccess)
                        {
                            decimal taxRate = QueryDatabase(countyName);
                            if (taxRate != -1)
                            {
                                SendSuccess(response, builder.SuccessData(CalculateResult(countyName, price, taxRate)));
                            }
                            else
                            {
                                SendFailure(response, Status.Failure("CountyError"), request.HttpMethod);
                            }
                        }
                        else
                        {
                            SendFailure(response, parseStatus, request.HttpMethod);
                        }
                    }
                    
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
