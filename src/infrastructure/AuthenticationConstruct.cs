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
            var cognitoResources = new CognitoResources(scope, id);
            var dynamodbResources = new DynamoDbResources(scope, id);
            var lambdaResources = new LambdaResources(
                scope,
                id,
                new Dictionary<string, string>
                {
                    {"USER_POOL_ID", cognitoResources.UserPool.UserPoolId },
                    {"USER_POOL_CLIENT_ID", cognitoResources.UserPoolClient.UserPoolClientId }
                });
            var apiGatewayResources = new ApiGatewayResources(scope, id, apiParent, lambdaResources);

            dynamodbResources.UsersTable.GrantReadWriteData(lambdaResources.CreateAccountFunction);
            lambdaResources.CreateAccountFunction.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new[] { "cognito-idp:InitiateAuth", "cognito-idp:SignUp", "cognito-idp:AdminConfirmSignUp" },
                Resources = new[] { cognitoResources.UserPool.UserPoolArn },
            }));

        }
    }
}
