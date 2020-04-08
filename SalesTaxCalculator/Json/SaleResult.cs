namespace SalesTaxCalculator.Json
{

    /// <summary>
    /// Class that will be sent in the JSON reponse after successful queries.
    /// </summary>
    public class SaleResult
    {
        public string County { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal SaleTotal { get; set; }
    }
}
