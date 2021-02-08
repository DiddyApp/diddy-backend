using System;
using Amazon.DynamoDBv2.DataModel;

namespace Goals.Models
{
    public class Task
    {
        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("deadline")]
        public DateTime? Deadline { get; set; }
    }
}
