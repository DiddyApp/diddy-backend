using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Goals.Models
{
    [DynamoDBTable(tableName: "Goals")]
    public class Goal
    {
        [DynamoDBHashKey("uid")]
        public string Uid { get; set; }

        [DynamoDBRangeKey("goal_id")]
        public string GoalId { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("counter")]
        public long Counter { get; set; }

        [DynamoDBProperty("tasks")]
        public List<Task> Tasks { get; set; }
    }
}
