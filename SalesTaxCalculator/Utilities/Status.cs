namespace SalesTaxCalculator
{

    /// <summary>
    /// A simulated discriminated union to provide more detailed error codes for methods.
    /// </summary>
    public class Status
    {
        public readonly bool isSuccess;
        public readonly string message;

        private Status(bool isSuccess, string message)
        {
            this.isSuccess = isSuccess;
            this.message = message;
        }

        /// <summary>
        /// The operation was successful.
        /// </summary>
        /// <returns>A Status object with isSuccess equal to true.</returns>
        public static Status Success()
        {
            return new Status(true, "");
        }

        /// <summary>
        /// The operation failed.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns>A Status object with isSuccess equal to false and containing the specified error message.</returns>
        public static Status Failure(string message)
        {
            return new Status(false, message);
        }
    }
}
