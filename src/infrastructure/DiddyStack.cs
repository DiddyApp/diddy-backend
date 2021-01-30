using Amazon.CDK;

namespace Infrastructure
{
    public class DiddyStack : Stack
    {
        internal DiddyStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var common = new CommonConstruct(this, $"{id}-common");
            new AuthenticationConstruct(this, $"{id}-authentication", common.ApiGatewayResources.ApiParent);
        }
    }
}
