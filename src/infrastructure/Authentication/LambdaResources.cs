using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace Infrastructure.Authentication
{
    public class LambdaResources : Construct
    {
        public LambdaResources(Construct scope, string id, Dictionary<string, string> environmentVariables)
            : base(scope, $"{id}-Lambda")
        {
            CreateAccountFunction = new Function(scope, "CreateAccount", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Authentication/publish"),
                Handler = "Authentication::Authentication.CreateAccountFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"USER_POOL_ID", environmentVariables["USER_POOL_ID"] },
                    {"USER_POOL_CLIENT_ID", environmentVariables["USER_POOL_CLIENT_ID"] },
                }
            });

            LoginFunction = new Function(scope, "Login", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Authentication/publish"),
                Handler = "Authentication::Authentication.LoginFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"USER_POOL_ID", environmentVariables["USER_POOL_ID"] },
                    {"USER_POOL_CLIENT_ID", environmentVariables["USER_POOL_CLIENT_ID"] },
                }
            });
        }

        public Function CreateAccountFunction { get; private set; }
        public Function LoginFunction { get; private set; }
    }
}
