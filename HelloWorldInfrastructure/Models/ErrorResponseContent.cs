namespace HelloWorldInfrastructure.Models
{
    /// <summary>
    ///     Model for error responses
    /// </summary>
    public class ErrorResponseContent
    {
        public string ErrorCode { get; set; }
        
        public string Message { get; set; }

        public string ExceptionType { get; set; }

        public string FullException { get; set; }

        public string Severity { get; set; }
    }
}
