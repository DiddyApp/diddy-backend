using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Common.LambdaUtils;

namespace Goals
{
    public class AddGoalFunction
    {
        public static string _goalsTableName = Environment.GetEnvironmentVariable("GOALS_TABLE_NAME");
        public static Table _goalsTable = null;

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                // Get the UID from the JWT
                request.RequestContext.Authorizer.Claims.TryGetValue("cognito:username", out string uid);

                // Load table globally for optimization, because of the DescribeTable call
                if (_goalsTable == null)
                {
                    var client = new AmazonDynamoDBClient();
                    _goalsTable = Table.LoadTable(client, _goalsTableName);
                }

                var goal = Document.FromJson(request.Body);
                goal["uid"] = uid;
                var result = await _goalsTable.PutItemAsync(goal);

                return new APIGatewayProxyResponse().CreateSuccessResponse(result.ToJson());
            } catch (Exception e)
            {
                return new APIGatewayProxyResponse().CreateErrorResponse(e.StackTrace);
            }
        } 
    }
}