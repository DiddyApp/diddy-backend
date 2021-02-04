using System;
using System.Collections.Generic;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
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
        public static AmazonCognitoIdentityProviderClient _provider = new AmazonCognitoIdentityProviderClient();
        public static CognitoUserPool _userPool = new CognitoUserPool(_userPoolId, _userPoolClientId, _provider);

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var userData = request.GetBody<CreateAccountRequestModel>();

            await _userPool.SignUpAsync(userData.Email, userData.Password, new Dictionary<string, string>(), new Dictionary<string, string>());
            await _provider.AdminConfirmSignUpAsync(new AdminConfirmSignUpRequest
            {
                Username = userData.Email,
                UserPoolId = _userPool.PoolID
            });
            var authResult = await _provider.InitiateAuthAsync(new InitiateAuthRequest
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