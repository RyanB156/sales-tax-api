using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator
{
    public class Status
    {
        public readonly bool isSuccess;
        public readonly string message;

        private Status(bool isSuccess, string message)
        {
            this.isSuccess = isSuccess;
            this.message = message;

            new Logger().Debug($"Status - IsSuccess: {isSuccess}, Message: {message}");
        }

        public static Status Success()
        {
            return new Status(true, "");
        }

        public static Status Failure(string message)
        {
            return new Status(false, message);
        }
    }
}
