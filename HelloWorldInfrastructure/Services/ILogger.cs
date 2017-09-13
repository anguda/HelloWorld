using System;
using System.Collections.Generic;

namespace HelloWorldInfrastructure.Services
{
    /// <summary>
    ///     Service for logging
    /// </summary>
    public interface ILogger
    {
        void Info(string message, Dictionary<string, object> otherProperties);
        
        void Debug(string message, Dictionary<string, object> otherProperties);
        
        void Error(string message, Dictionary<string, object> otherProperties, Exception exception);
    }
}