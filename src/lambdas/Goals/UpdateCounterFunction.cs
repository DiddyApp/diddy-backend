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
    public class UpdateCounterFunction
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

                // Deserialize the request
                var deltaGoal = JsonConvert.DeserializeObject<DeltaGoal>(request.Body);

                // Get the current Goal value stored in DB
                var currentGoalState = await _dynamoDbContext.LoadAsync<Goal>(uid);

                // Update the counter value
                currentGoalState.Counter += deltaGoal.Delta;

                // Save the updated value to DB
                await _dynamoDbContext.SaveAsync(currentGoalState);

                // Return the updated Goal 
                return new APIGatewayProxyResponse().CreateSuccessResponse(currentGoalState);
            }
            catch (Exception e)
            {
                LambdaLogger.Log(e.ToString());
                return new APIGatewayProxyResponse().CreateErrorResponse(e.ToString());
            }
        }
    }
}