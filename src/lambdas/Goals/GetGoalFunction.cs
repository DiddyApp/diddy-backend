using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Common.LambdaUtils;

namespace Goals
{
    public class GetGoalFunction
    {
        public static string _goalsTableName = Environment.GetEnvironmentVariable("GOALS_TABLE_NAME");
        public static Table _goalsTable = null;

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            // Get the UID from the JWT
            string uid = string.Empty;
            request.RequestContext.Authorizer.Claims.TryGetValue("cognito:username", out uid);

            // Load table globally for optimization, because of the DescribeTable call
            if (_goalsTable == null)
            {
                var client = new AmazonDynamoDBClient();
                _goalsTable = Table.LoadTable(client, _goalsTableName);
            }

            if (request.QueryStringParameters.ContainsKey("id"))
            {
                var goalId = request.QueryStringParameters["id"];
                var result = await _goalsTable.GetItemAsync(uid, goalId);
                return new APIGatewayProxyResponse().CreateSuccessResponse(result);
            }
            else
            {
                var result = await _goalsTable.Query(uid, new QueryFilter()).GetNextSetAsync();
                return new APIGatewayProxyResponse().CreateSuccessResponse(result);
            }
        }
    }
}