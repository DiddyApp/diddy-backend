using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Cognito;

namespace Infrastructure.Goals
{
    public class ApiGatewayResources : Construct
    {
        public ApiGatewayResources(
            Construct scope,
            string id,
            Amazon.CDK.AWS.APIGateway.Resource apiParent,
            LambdaResources lambdas,
            UserPool userPool)
           : base(scope, $"{id}-ApiGateway")
        {
            var goalsResource = apiParent.AddResource("goals");
            var authorizer = new CfnAuthorizer(
                this,
                $"{id}-Goals-Auth",
                new CfnAuthorizerProps
                {
                    Name = $"{id}-Goals-Authorizer",
                    Type = "COGNITO_USER_POOLS",
                    RestApiId = goalsResource.Api.RestApiId,
                    ProviderArns = new string[] { userPool.UserPoolArn}
                });

            var addGoalIntegration = new LambdaIntegration(lambdas.AddGoal, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });
            goalsResource.AddMethod("POST", addGoalIntegration, new MethodOptions
            {
                AuthorizationType = AuthorizationType.COGNITO,
            });

            var getGoalIntegration = new LambdaIntegration(lambdas.GetGoal, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });
            goalsResource.AddMethod("GET", getGoalIntegration, new MethodOptions
            {
                AuthorizationType = AuthorizationType.IAM
            });

            var deleteGoalIntegration = new LambdaIntegration(lambdas.DeleteGoal, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });
            goalsResource.AddMethod("DELETE", deleteGoalIntegration, new MethodOptions
            {
                AuthorizationType = AuthorizationType.IAM
            });

            var updateGoalIntegration = new LambdaIntegration(lambdas.UpdateGoal, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });
            goalsResource.AddMethod("PUT", updateGoalIntegration, new MethodOptions
            {
                AuthorizationType = AuthorizationType.IAM
            });

        }
    }
}
