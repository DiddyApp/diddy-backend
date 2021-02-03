using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace Infrastructure.Goals
{
    public class LambdaResources : Construct
    {
        public LambdaResources(Construct scope, string id, Dictionary<string, string> environmentVariables)
            : base(scope, $"{id}-Lambda")
        {
            AddGoal = new Function(scope, "AddGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.AddGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"GOALS_TABLE_NAME", environmentVariables["GOALS_TABLE_NAME"]}
                }
            });

            GetGoal = new Function(scope, "GetGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.GetGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"GOALS_TABLE_NAME", environmentVariables["GOALS_TABLE_NAME"]}
                }
            });

            DeleteGoal = new Function(scope, "DeleteGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.DeleteGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"GOALS_TABLE_NAME", environmentVariables["GOALS_TABLE_NAME"]}
                }
            });

            UpdateGoal = new Function(scope, "UpdateCounter", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.UpdateCounterFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"GOALS_TABLE_NAME", environmentVariables["GOALS_TABLE_NAME"]}
                }
            });
        }

        public Function GetGoal { get; set; }
        public Function AddGoal { get; set; }
        public Function UpdateGoal { get; set; }
        public Function DeleteGoal { get; set; }

    }
}
