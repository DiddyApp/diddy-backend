using System;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace Common.LambdaUtils
{
    public static class APIGatewayProxyRequestExtensions
    {
        public static T GetBody<T>(this APIGatewayProxyRequest request)
        {
            var body = JsonConvert.DeserializeObject<T>(request.Body);
            return body;
        }
    }
}
