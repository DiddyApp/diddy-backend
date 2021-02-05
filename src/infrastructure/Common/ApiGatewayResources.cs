using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;

namespace Infrastructure.Common
{
    public class ApiGatewayResources : Construct
    {
        public ApiGatewayResources(Construct scope, string id) : base(scope, $"{id}-ApiGateway")
        {
            var api = new RestApi(this, "Diddy-API", new RestApiProps
            {
                RestApiName = "Diddy API"
            });

            var parent = api.Root.AddResource("api");
            ApiParent = parent.AddResource("v1");
        }

        public Amazon.CDK.AWS.APIGateway.Resource ApiParent { get; set; }
    }
}
