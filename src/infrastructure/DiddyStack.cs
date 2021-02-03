using Amazon.CDK;

namespace Infrastructure
{
    public class DiddyStack : Stack
    {
        internal DiddyStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var common = new CommonConstruct(
                this,
                $"{id}-common");

            var authConstruct = new AuthenticationConstruct(
                this,
                $"{id}-authentication",
                common.ApiGatewayResources.ApiParent);

            var goalsConstruct = new GoalsConstruct(
                this,
                $"{id}-goals",
                authConstruct.CognitoResources.UserPool,
                common.ApiGatewayResources.ApiParent);
        }
    }
}
