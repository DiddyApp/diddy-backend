using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Common.LambdaUtils;
using Goals.Models;
using Newtonsoft.Json;

namespace Goals
{
    public class DeleteGoalFunction
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

                // Get the GoalId from Query
                var goalId = request.QueryStringParameters["id"];

                // Delete item using HashKey and RangeKey
                await _dynamoDbContext.DeleteAsync<Goal>(uid, goalId);

                // Return response
                return new APIGatewayProxyResponse().CreateSuccessResponse("Delete Successful");

            }
            catch (Exception e)
            {
                LambdaLogger.Log(e.ToString());
                return new APIGatewayProxyResponse().CreateErrorResponse(e.ToString());
            }
        }
    }
}