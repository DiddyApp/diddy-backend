using System;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace CreateAccount.LambdaUtils

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
