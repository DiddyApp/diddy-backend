using System;
using Amazon.DynamoDBv2.DataModel;

namespace Goals.Models
{
    [DynamoDBTable(tableName: "Goals")]
    public class Goal
    {
        [DynamoDBHashKey]
        public string Uid { get; set; }

        [DynamoDBProperty]
        public string Id { get; set; }

        [DynamoDBProperty]
        public long Counter { get; set; }
    }
}
