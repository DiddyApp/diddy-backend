using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Runtime.Internal.Util;
using Common.LambdaUtils;
using Goals.Models;

namespace Goals
{
    public class GetGoalFunction
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

                if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey("id"))
                {
                    var goalId = request.QueryStringParameters["id"];
                    var result = await _dynamoDbContext.LoadAsync(uid);
                    return new APIGatewayProxyResponse().CreateSuccessResponse(result);
                }
                else
                {
                    var result = await _dynamoDbContext.QueryAsync<Goal>(uid).GetNextSetAsync();
                    return new APIGatewayProxyResponse().CreateSuccessResponse(result);
                }
            } catch (Exception e)
            {
                LambdaLogger.Log(e.ToString());
                return new APIGatewayProxyResponse().CreateErrorResponse(e.ToString());
            }
        }
    }
}