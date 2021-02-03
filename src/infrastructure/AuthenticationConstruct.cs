using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Infrastructure.Authentication;

namespace Infrastructure
{
    public class AuthenticationConstruct : Construct
    {
        public AuthenticationConstruct(Construct scope, string id, Amazon.CDK.AWS.APIGateway.Resource apiParent)
            : base(scope, id)
        {
            CognitoResources = new CognitoResources(scope, id);
            DynamoDbResources = new DynamoDbResources(scope, id);
            LambdaResources = new LambdaResources(
                scope,
                id,
                new Dictionary<string, string>
                {
                    {"USER_POOL_ID", CognitoResources.UserPool.UserPoolId },
                    {"USER_POOL_CLIENT_ID", CognitoResources.UserPoolClient.UserPoolClientId }
                });
            ApiGatewayResources = new ApiGatewayResources(scope, id, apiParent, LambdaResources);

            DynamoDbResources.UsersTable.GrantReadWriteData(LambdaResources.CreateAccountFunction);

            LambdaResources.CreateAccountFunction.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new[] { "cognito-idp:InitiateAuth", "cognito-idp:SignUp", "cognito-idp:AdminConfirmSignUp" },
                Resources = new[] { CognitoResources.UserPool.UserPoolArn },
            }));

            LambdaResources.LoginFunction.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new[] { "cognito-idp:InitiateAuth"},
                Resources = new[] { CognitoResources.UserPool.UserPoolArn },
            }));
        }

        public CognitoResources CognitoResources { get; set; }
        public DynamoDbResources DynamoDbResources { get; set; }
        public LambdaResources LambdaResources { get; set; }
        public ApiGatewayResources ApiGatewayResources { get; set; }
    }
}
