using System;
using System.Collections.Generic;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Common.LambdaUtils;
using Authentication.Models;
using Amazon.CognitoIdentityProvider.Model;
using System.Threading.Tasks;

namespace Authentication
{
    public class CreateAccountFunction
    {
        public static string _userPoolId = Environment.GetEnvironmentVariable("USER_POOL_ID");
        public static string _userPoolClientId = Environment.GetEnvironmentVariable("USER_POOL_CLIENT_ID");


        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var userData = request.GetBody<CreateAccountRequestModel>();

            var provider = new AmazonCognitoIdentityProviderClient();
            var userPool = new CognitoUserPool(_userPoolId, _userPoolClientId, provider);

            await userPool.SignUpAsync(userData.Email, userData.Password, null, null);
            await provider.ConfirmSignUpAsync(new ConfirmSignUpRequest()
            {
                Username = userData.Email,
                ClientId = _userPoolClientId,
            });
            var authResult = await provider.InitiateAuthAsync(new InitiateAuthRequest
            {
                AuthParameters = new Dictionary<string, string>
                {
                    {"USERNAME", userData.Email },
                    {"PASSWORD", userData.Password }
                },
                ClientId = _userPoolClientId
            });

            var response = new CreateAccountResponseModel
            {
                IdToken = authResult.AuthenticationResult.IdToken,
                RefreshToken = authResult.AuthenticationResult.RefreshToken
            };

            return new APIGatewayProxyResponse().CreateSuccessResponse(response);
        }
    }
}