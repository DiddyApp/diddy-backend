using Amazon.CDK;

namespace Infrastructure
{
    public class LambdasStack : Stack
    {
        internal LambdasStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
        }
    }
}