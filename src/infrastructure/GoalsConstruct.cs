using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class GoalsConstruct : Construct
    {
        public GoalsConstruct(Construct scope, string id) : base(scope, id)
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

            var goalsTable = new Table(scope, "GoalsTable",
                new TableProps
                {
                    TableName = "Goals",
                    BillingMode = BillingMode.PAY_PER_REQUEST, 
                    PartitionKey = new Attribute
                    {
                        Name = "id",
                        Type = AttributeType.STRING
                    },
                });

            // Add Goal
            var addGoalFunction = new Function(scope, "AddGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.AddGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"USER_POOL_ID", userPool.UserPoolId },
                    {"USER_POOL_CLIENT_ID", userPoolClient.UserPoolClientId },
                }
            });

            goalsTable.GrantReadWriteData(addGoalFunction);
            addGoalFunction.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new[] { "cognito-idp:InitiateAuth", "cognito-idp:SignUp", "cognito-idp:AdminConfirmSignUp" },
                Resources = new[] { userPool.UserPoolArn },
            }));

            var addGoalApi = new RestApi(scope, "AddGoal-API", new RestApiProps
            {
                RestApiName = "AddGoal Service"
            });

            var addGoalIntegration = new LambdaIntegration(addGoalFunction, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });

            addGoalApi.Root.AddMethod("POST", addGoalIntegration);

            // Get Goal
            var getGoalFunction = new Function(scope, "GetGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.GetGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"USER_POOL_ID", userPool.UserPoolId },
                    {"USER_POOL_CLIENT_ID", userPoolClient.UserPoolClientId },
                }
            });

            goalsTable.GrantReadData(getGoalFunction);
            getGoalFunction.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new[] { "cognito-idp:InitiateAuth", "cognito-idp:SignUp", "cognito-idp:AdminConfirmSignUp" },
                Resources = new[] { userPool.UserPoolArn },
            }));

            var getGoalApi = new RestApi(scope, "GetGoal-API", new RestApiProps
            {
                RestApiName = "GetGoal Service"
            });

            var getGoalIntegration = new LambdaIntegration(getGoalFunction, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });

            getGoalApi.Root.AddMethod("GET", getGoalIntegration);

            // Delete Goal
            var deleteGoalFunction = new Function(scope, "DeleteGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.DeleteGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"USER_POOL_ID", userPool.UserPoolId },
                    {"USER_POOL_CLIENT_ID", userPoolClient.UserPoolClientId },
                }
            });

            goalsTable.GrantReadWriteData(deleteGoalFunction);
            deleteGoalFunction.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new[] { "cognito-idp:InitiateAuth", "cognito-idp:SignUp", "cognito-idp:AdminConfirmSignUp" },
                Resources = new[] { userPool.UserPoolArn },
            }));

            var deleteGoalApi = new RestApi(scope, "DeleteGoal-API", new RestApiProps
            {
                RestApiName = "DeleteGoal Service"
            });

            var deleteGoalIntegration = new LambdaIntegration(deleteGoalFunction, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });

            deleteGoalApi.Root.AddMethod("DELETE", deleteGoalIntegration);

            // Update Goal
            var updateGoalFunction = new Function(scope, "UpdateGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.UpdateGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"USER_POOL_ID", userPool.UserPoolId },
                    {"USER_POOL_CLIENT_ID", userPoolClient.UserPoolClientId },
                }
            });

            goalsTable.GrantReadWriteData(updateGoalFunction);
            updateGoalFunction.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Actions = new[] { "cognito-idp:InitiateAuth", "cognito-idp:SignUp", "cognito-idp:AdminConfirmSignUp" },
                Resources = new[] { userPool.UserPoolArn },
            }));

            var updateGoalApi = new RestApi(scope, "UpdateGoal-API", new RestApiProps
            {
                RestApiName = "UpdateGoal Service"
            });

            var updateGoalIntegration = new LambdaIntegration(updateGoalFunction, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });

            updateGoalApi.Root.AddMethod("POST", updateGoalIntegration);
        }
    }
}
