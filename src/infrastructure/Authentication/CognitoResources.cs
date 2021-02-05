using System;
using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;

namespace Infrastructure.Authentication
{
    public class CognitoResources : Construct
    {
        public CognitoResources(Construct scope, string id) : base(scope, $"{id}-Cognito")
        {
            UserPool = new UserPool(this, "UserPool");
            UserPoolClient = new UserPoolClient(
                scope,
                "UserPoolClient",
                new UserPoolClientProps
                {
                    UserPoolClientName = "User Pool Client",
                    UserPool = UserPool,
                    GenerateSecret = false,
                    PreventUserExistenceErrors = true,
                    AuthFlows = new AuthFlow
                    {
                        UserPassword = true,
                        AdminUserPassword = true,
                        UserSrp = true,
                        Custom = true
                    },
                });
        }

        public UserPool UserPool { get; private set; }
        public UserPoolClient UserPoolClient { get; private set; }
    }
}
