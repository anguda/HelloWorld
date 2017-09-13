using System;
using HelloWorldInfrastructure.Mappers;
using HelloWorldInfrastructure.Resources;
using HelloWorldInfrastructure.Services;
using HelloWorldInfrastructure.Models;
using RestSharp;

namespace MyHelloWorld.Service
{
    public class HelloWorldWebService : IHelloWorldWebService
    {
        private readonly IAppSettings appSettings;
        
        private readonly ILogger logger;
        
        private readonly IRestClient restClient;
        
        private readonly IRestRequest restRequest;
        
        private readonly IUri uriService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldWebService" /> class.
        /// </summary>
        
        public HelloWorldWebService(
            IRestClient restClient,
            IRestRequest restRequest,
            IAppSettings appSettings,
            IUri uriService,
            ILogger logger)
        {
            this.restClient = restClient;
            this.restRequest = restRequest;
            this.appSettings = appSettings;
            this.uriService = uriService;
            this.logger = logger;
        }

        /// <summary>
        ///     Gets data from the web API
        /// </summary>
        /// <returns>A model containing  data</returns>
        public HelloData GetData()
        {
            HelloData data = null;

            // Set the URL for the request
            this.restClient.BaseUrl = this.uriService.GetUri(this.appSettings.Get(AppSettingsKeys.HelloWorldApiUrlKey));

            // Setup the request
            this.restRequest.Resource = "data";
            this.restRequest.Method = Method.GET;

            // Clear the request parameters
            this.restRequest.Parameters.Clear();

            // Execute the call and get the response
            var dataResponse = this.restClient.Execute<HelloData>(this.restRequest);

            // Check for data in the response
            if (dataResponse != null)
            {
                // Check if any actual data was returned
                if (dataResponse.Data != null)
                {
                    data = dataResponse.Data;
                }
                else
                {
                    var errorMessage = "Error in RestSharp" + " Error message: "
                                       + dataResponse.ErrorMessage + " HTTP Status Code: "
                                       + dataResponse.StatusCode + " HTTP Status Description: "
                                       + dataResponse.StatusDescription;

                    // Check for existing exception
                    if (dataResponse.ErrorMessage != null && dataResponse.ErrorException != null)
                    {
                        // Log an informative exception including the RestSharp exception
                        this.logger.Error(errorMessage, null, dataResponse.ErrorException);
                    }
                    else
                    {
                        // Log an informative exception including the RestSharp content
                        this.logger.Error(errorMessage, null, new Exception(dataResponse.Content));
                    }
                }
            }
            else
            {
                // Log the exception
                const string ErrorMessage =
                    "Did not get any response from the Hello World Web Api for the Method: GET /data";

                this.logger.Error(ErrorMessage, null, new Exception(ErrorMessage));
            }

            return data;
        }
    }
}
