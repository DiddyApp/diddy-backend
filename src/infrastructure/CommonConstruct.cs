using System;
using Amazon.CDK;
using Infrastructure.Common;

namespace Infrastructure
{
    public class CommonConstruct : Construct
    {
        public CommonConstruct(Construct scope, string id)
            : base(scope, id)
        {
            ApiGatewayResources = new ApiGatewayResources(this, $"{id}-ApiGateway");
            LambdaResources = new LambdaResources(this, $"{id}-Lambda");
        }

        public ApiGatewayResources ApiGatewayResources { get; private set; }
        public LambdaResources LambdaResources { get; set; }
    }
}
