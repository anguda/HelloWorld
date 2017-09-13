using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using HelloWorldInfrastructure.Models;
using HelloWorldInfrastructure.Resources;
using HelloWorldInfrastructure.Services;

namespace HelloWorldInfrastructure.Attributes
{
    /// <summary>
    ///     Severity code enumeration
    /// </summary>
    public enum SeverityCode
    {
        /// <summary>
        ///     No severity level
        /// </summary>
        None,

        /// <summary>
        ///     Information severity level
        /// </summary>
        Information,

        /// <summary>
        ///     Warning severity level
        /// </summary>
        Warning,

        /// <summary>
        ///     Error severity level
        /// </summary>
        Error,

        /// <summary>
        ///     Critical severity level
        /// </summary>
        Critical
    }

    /// <summary>
    ///     A customer exception filter attribute class for globally handling exceptions and creating a Http Response Message
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public Type Type { get; set; }
        
        public HttpStatusCode Status { get; set; }

        public SeverityCode Severity { get; set; }
        
        public ILogger Logger { get; set; }

        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            // If the exception type matches
            if (exception.GetType() == this.Type)
            {
                // Get the inner exception message if it exists
                var innerMessage = context.Exception.InnerException != null
                                       ? context.Exception.InnerException.Message
                                       : context.Exception.Message;

                // Create the error response without the stack trace/full exception (It can contain server path information)
                context.Response = context.Request.CreateResponse(
                    this.Status,
                    new ErrorResponseContent
                    {
                        ErrorCode = context.Exception.Message,
                        Message = innerMessage,
                        ExceptionType = context.Exception.GetType().ToString(),
                        FullException = string.Empty,
                        Severity = this.Severity.ToString()
                    });

                // Log the error (including the stack trace/full exception)
                this.Logger.Error(innerMessage, null, context.Exception);
            }
            else
            {
   
                if ((this.Type == null) && (context.Response == null))
                {
                    // Unhandled exception (Critical InternalServerError)
                    context.Response = context.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new ErrorResponseContent
                        {
                            ErrorCode = ErrorCodes.GeneralError,
                            Message = context.Exception.Message,
                            ExceptionType = context.Exception.GetType().ToString(),
                            FullException = string.Empty,
                            Severity = SeverityCode.Critical.ToString()
                        });

                    // Log the error
                    this.Logger.Error(ErrorCodes.GeneralError, null, context.Exception);
                }
            }
        }
    }
}