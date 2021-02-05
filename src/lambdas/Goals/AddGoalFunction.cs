using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Common.LambdaUtils;
using Goals.Models;
using Newtonsoft.Json;

namespace Goals
{
    public class AddGoalFunction
    {
        public static string _goalsTableName = Environment.GetEnvironmentVariable("GOALS_TABLE_NAME");
        public static DynamoDBContext _dynamoDbContext = null;

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                // Get the UID from the JWT
                request.RequestContext.Authorizer.Claims.TryGetValue("cognito:username", out string uid);

                // Load table globally for optimization, because of the DescribeTable call
                if (_dynamoDbContext == null)
                {
                    var client = new AmazonDynamoDBClient();
                    _dynamoDbContext = new DynamoDBContext(client);
                }

                var goal = JsonConvert.DeserializeObject<Goal>(request.Body);
                goal.Uid = uid;
                await _dynamoDbContext.SaveAsync(goal);
                return new APIGatewayProxyResponse().CreateSuccessResponse(goal);
            }
            catch (Exception e)
            {
                LambdaLogger.Log(e.ToString());
                return new APIGatewayProxyResponse().CreateErrorResponse(e.ToString());
            }
        }
    }
}