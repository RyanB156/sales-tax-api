namespace SalesTaxCalculator.Json
{

    /// <summary>
    /// Class to hold the incoming data from POST requests.
    /// </summary>
    public class InputData
    {
        public string County { get; set; }
        public decimal Price { get; set; }
    }
}
