using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Infrastructure.Goals;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class GoalsConstruct : Construct
    {
        public GoalsConstruct(
            Construct scope,
            string id,
            UserPool userPool,
            Amazon.CDK.AWS.APIGateway.Resource apiParent,
            LayerVersion commonLayer)
            : base(scope, id)
        {
            var dynamoDb = new DynamoDbResources(this, id);
            var lambdaResources = new LambdaResources(this, id, new Dictionary<string, string>
            {
                { "GOALS_TABLE_NAME", dynamoDb.GoalsTable.TableName }
            },
            commonLayer);
            var apiGatewayResources = new ApiGatewayResources(this, id, apiParent, lambdaResources, userPool);

            dynamoDb.GoalsTable.Grant(lambdaResources.AddGoal, new string[] { "dynamodb:DescribeTable", "dynamodb:PutItem", "dynamodb:UpdateItem" });
            dynamoDb.GoalsTable.Grant(lambdaResources.GetGoal, new string[] { "dynamodb:DescribeTable", "dynamodb:GetItem", "dynamodb:Query" });
            dynamoDb.GoalsTable.GrantReadWriteData(lambdaResources.DeleteGoal);
            dynamoDb.GoalsTable.GrantReadWriteData(lambdaResources.UpdateGoal);
        }
    }
}
