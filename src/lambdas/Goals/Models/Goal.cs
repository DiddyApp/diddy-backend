using System;
using Amazon.DynamoDBv2.DataModel;

namespace Goals.Models
{
    [DynamoDBTable(tableName: "Goals")]
    public class Goal
    {
        [DynamoDBHashKey(attributeName:"uid")]
        public string Uid { get; set; }

        [DynamoDBRangeKey(attributeName:"goal_id")]
        public string GoalId { get; set; }

        [DynamoDBProperty(attributeName:"counter")]
        public long Counter { get; set; }
    }
}
