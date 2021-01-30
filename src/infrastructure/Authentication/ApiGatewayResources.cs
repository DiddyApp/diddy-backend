using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;

namespace Infrastructure.Authentication
{
    public class ApiGatewayResources : Construct
    {
        public ApiGatewayResources(Construct scope, string id, Amazon.CDK.AWS.APIGateway.Resource apiParent, LambdaResources lambdas)
            : base(scope, $"{id}-ApiGateway")
        {
            var api = new RestApi(scope, "Authentication-API", new RestApiProps
            {
                RestApiName = "Authentication Service"
            });

            var createAccountIntegration = new LambdaIntegration(lambdas.CreateAccountFunction, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });

            var authResource = apiParent.AddResource("/auth");
            authResource.AddMethod("POST", createAccountIntegration);
        }
    }
}
