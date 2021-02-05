using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace Infrastructure.Common
{
    public class LambdaResources : Construct
    {
        public LambdaResources(Construct scope, string id)
            : base(scope, $"{id}-Lambda")
        {
            CommonLayer = new LayerVersion(this, $"{id}-CommonLayer", new LayerVersionProps
            {
                Code = Code.FromAsset("lambdas/Common/publish"),
                CompatibleRuntimes = new Runtime[] { Runtime.DOTNET_CORE_3_1 },
                Description = "A layer that contains common models, utils, and NuGet packages."
            });
        }

        public LayerVersion CommonLayer { get; set; }
    }
}
