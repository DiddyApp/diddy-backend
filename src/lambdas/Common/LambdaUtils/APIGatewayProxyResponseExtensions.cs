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

            var response = new APIGatewayProxyResponse
            {
                Body = body,
                StatusCode = 200,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };
            return response;
        }
    }
}
