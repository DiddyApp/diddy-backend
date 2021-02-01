using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;

namespace Infrastructure.Goals
{
    public class ApiGatewayResources : Construct
    {
        public ApiGatewayResources(Construct scope, string id, Amazon.CDK.AWS.APIGateway.Resource apiParent, LambdaResources lambdas)
           : base(scope, $"{id}-ApiGateway")
        {
            var goalsResource = apiParent.AddResource("goals");
            var addGoalIntegration = new LambdaIntegration(lambdas.AddGoal, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });
            goalsResource.AddMethod("POST", addGoalIntegration);

            var getGoalIntegration = new LambdaIntegration(lambdas.GetGoal, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });
            goalsResource.AddMethod("GET", getGoalIntegration);

            var deleteGoalIntegration = new LambdaIntegration(lambdas.DeleteGoal, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });
            goalsResource.AddMethod("DELETE", deleteGoalIntegration);

            var updateGoalIntegration = new LambdaIntegration(lambdas.UpdateGoal, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });
            goalsResource.AddMethod("PUT", updateGoalIntegration);

        }
    }
}
