using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace Infrastructure.Goals
{
    public class LambdaResources : Construct
    {
        public LambdaResources(
            Construct scope,
            string id,
            Dictionary<string, string> environmentVariables,
            LayerVersion layer)
            : base(scope, $"{id}-Lambda")
        {
            AddGoal = new Function(this, "AddGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.AddGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"GOALS_TABLE_NAME", environmentVariables["GOALS_TABLE_NAME"]}
                },
                Layers = new LayerVersion[] { layer },
                Timeout = Duration.Seconds(15) // until we optimize this :)
            });

            GetGoal = new Function(this, "GetGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.GetGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"GOALS_TABLE_NAME", environmentVariables["GOALS_TABLE_NAME"]}
                },
                Layers = new LayerVersion[] { layer },
                Timeout = Duration.Seconds(15) // until we optimize this :)
            });

            DeleteGoal = new Function(this, "DeleteGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.DeleteGoalFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"GOALS_TABLE_NAME", environmentVariables["GOALS_TABLE_NAME"]}
                },
                Layers = new LayerVersion[] { layer },
                Timeout = Duration.Seconds(15) // until we optimize this :)
            });

            UpdateGoal = new Function(this, "UpdateCounter", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.UpdateCounterFunction::FunctionHandler",
                Environment = new Dictionary<string, string>
                {
                    {"GOALS_TABLE_NAME", environmentVariables["GOALS_TABLE_NAME"]}
                },
                Layers = new LayerVersion[] { layer },
                Timeout = Duration.Seconds(15) // until we optimize this :)
            });
        }

        public Function GetGoal { get; set; }
        public Function AddGoal { get; set; }
        public Function UpdateGoal { get; set; }
        public Function DeleteGoal { get; set; }

    }
}
