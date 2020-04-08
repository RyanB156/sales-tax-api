using Newtonsoft.Json;

namespace SalesTaxCalculator.Json
{

    /// <summary>
    /// Build JSON strings for the given types.
    /// </summary>
    public class JsonResponseBuilder
    {

        public string SuccessData(SaleResult result)
        {
            return JsonConvert.SerializeObject(result);
        }

        public string ErrorMessage(Status status)
        {
            ErrorMessage e = new ErrorMessage() { Error = status.message };
            return JsonConvert.SerializeObject(e);
        }

    }
}
