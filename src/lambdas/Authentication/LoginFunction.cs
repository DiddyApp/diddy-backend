using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Common.LambdaUtils;
using Amazon.Lambda.Core;
using Authentication.Models;
using Amazon.CognitoIdentityProvider;
using System.Collections.Generic;

namespace Authentication
{
    public class LoginFunction
    {
        public static string _userPoolId = Environment.GetEnvironmentVariable("USER_POOL_ID");
        public static string _userPoolClientId = Environment.GetEnvironmentVariable("USER_POOL_CLIENT_ID");

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var userData = request.GetBody<LoginRequestModel>();

            var provider = new AmazonCognitoIdentityProviderClient();

            var authResult = await provider.InitiateAuthAsync(new Amazon.CognitoIdentityProvider.Model.InitiateAuthRequest
            {
                AuthParameters = new Dictionary<string, string>
                {
                    {"USERNAME", userData.Email },
                    {"PASSWORD", userData.Password }
                },
                ClientId = _userPoolClientId,
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH
            });

            var response = new UserAuthResponseModel
            {
                UserId = userData.Email,
                IdToken = authResult.AuthenticationResult.IdToken,
                RefreshToken = authResult.AuthenticationResult.RefreshToken
            };

            return new APIGatewayProxyResponse().CreateSuccessResponse(response);
        }
    }
}