using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Common.LambdaUtils;
using Amazon.Lambda.Core;

namespace Authentication
{
    public class LoginFunction
    {
        public static string _userPoolId = Environment.GetEnvironmentVariable("USER_POOL_ID");
        public static string _userPoolClientId = Environment.GetEnvironmentVariable("USER_POOL_CLIENT_ID");

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            return new APIGatewayProxyResponse().CreateSuccessResponse("test");
        }
    }
}