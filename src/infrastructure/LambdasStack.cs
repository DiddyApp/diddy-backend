using Amazon.CDK;

namespace Infrastructure
{
    public class LambdasStack : Stack
    {
        internal LambdasStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            new AuthenticationConstruct(this, $"{id}-authentication");
            new GoalsConstruct(this, $"{id}-goal");
        }
    }
}
