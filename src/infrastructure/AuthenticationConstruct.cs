using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;

namespace Infrastructure
{
    public class AuthenticationConstruct : Construct
    {
        public AuthenticationConstruct(Construct scope, string id) : base(scope, id)
        {
            var userPool = new UserPool(scope, "UserPool");
            var userPoolClient = new UserPoolClient(scope, "UserPoolClient",
                new UserPoolClientProps
                {
                    UserPoolClientName = "User Pool Client",
                    UserPool = userPool,
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
            var usersTable = new Table(scope, "UsersTable",
                new TableProps
                {
                    TableName = "Users",
                    BillingMode = BillingMode.PAY_PER_REQUEST, // will probably be changes to Provisioned in production
                    PartitionKey = new Attribute
                    {
                        Name = "id",
                        Type = AttributeType.STRING
                    },
                });

            var createAccountFunction = new Function(scope, "CreateAccount", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Authentication/publish"),
                Handler = "Authentication::Authentication.CreateAccountFunction::FunctionHandler"
            });

            usersTable.GrantReadWriteData(createAccountFunction);
            createAccountFunction.AddToRolePolicy(new Amazon.CDK.AWS.IAM.PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new[] { "cognito-idp:InitiateAuth" },
                Resources = new[] { userPool.UserPoolArn },
            }));

            //TODO: code for the AttachIdentity function; needs more research

            var api = new RestApi(scope, "Authentication-API", new RestApiProps
            {
                RestApiName = "Authentication Service"
            });

            var createAccountIntegration = new LambdaIntegration(createAccountFunction, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });

            api.Root.AddMethod("GET", createAccountIntegration);
        }
    }
}
