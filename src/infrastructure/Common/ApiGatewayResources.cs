using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;

namespace Infrastructure.Common
{
    public class ApiGatewayResources : Construct
    {
        public ApiGatewayResources(Construct scope, string id) : base(scope, $"{id}-ApiGateway")
        {
            var api = new RestApi(scope, "Diddy-API", new RestApiProps
            {
                RestApiName = "Authentication Service"
            });

            var parent = api.Root.AddResource("/api");
            parent.AddResource("v1");
        }

        public Amazon.CDK.AWS.APIGateway.Resource ApiParent { get; set; }
    }
}
