using System;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace Infrastructure.Goals
{
    public class LambdaResources : Construct
    {
        public LambdaResources(Construct scope, string id)
            : base(scope, $"{id}-Lambda")
        {
            AddGoal = new Function(scope, "AddGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.AddGoalFunction::FunctionHandler",
            });

            GetGoal = new Function(scope, "GetGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.GetGoalFunction::FunctionHandler",
            });

            DeleteGoal = new Function(scope, "DeleteGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.DeleteGoalFunction::FunctionHandler",
            });

            UpdateGoal = new Function(scope, "UpdateGoal", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Goals/publish"),
                Handler = "Goals::Goals.UpdateGoalFunction::FunctionHandler",
            });
        }

        public Function GetGoal { get; set; }
        public Function AddGoal { get; set; }
        public Function UpdateGoal { get; set; }
        public Function DeleteGoal { get; set; }

    }
}
