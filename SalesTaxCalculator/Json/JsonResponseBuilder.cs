using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SalesTaxCalculator.Json
{
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
