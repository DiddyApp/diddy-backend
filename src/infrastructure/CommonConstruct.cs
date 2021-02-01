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
            ApiGatewayResources = new ApiGatewayResources(scope, $"{id}-ApiGateway");
        }

        public ApiGatewayResources ApiGatewayResources { get; private set; }
    }
}
