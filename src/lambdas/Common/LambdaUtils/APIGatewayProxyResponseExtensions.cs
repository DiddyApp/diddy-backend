using System;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace Common.LambdaUtils
{
    public static class APIGatewayProxyResponseExtensions
    {
        public static APIGatewayProxyResponse CreateSuccessResponse<T>(this APIGatewayProxyResponse apiResponse, T bodyContent)
        {
            var body = JsonConvert.SerializeObject(bodyContent);

            apiResponse.Body = body;
            apiResponse.StatusCode = 200;
            apiResponse.Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Access-Control-Allow-Origin", "*" }
            };

            return apiResponse;
        }

        public static APIGatewayProxyResponse CreateSuccessResponse(this APIGatewayProxyResponse apiResponse, string bodyContent)
        {
            apiResponse.Body = bodyContent;
            apiResponse.StatusCode = 200;
            apiResponse.Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Access-Control-Allow-Origin", "*" }
            };

            return apiResponse;
        }

        public static APIGatewayProxyResponse CreateErrorResponse(this APIGatewayProxyResponse apiResponse, string bodyContent)
        {
            apiResponse.Body = JsonConvert.SerializeObject(new { error = bodyContent});
            apiResponse.StatusCode = 400;
            apiResponse.Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Access-Control-Allow-Origin", "*" }
            };

            return apiResponse;
        }
    }
}
