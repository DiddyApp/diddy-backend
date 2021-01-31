using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;

namespace Infrastructure.Goals
{
    public class DynamoDbResources : Construct
    {
        public DynamoDbResources(Construct scope, string id) : base(scope, $"{id}-DynamoDb")
        {
            GoalsTable = new Table(scope, "GoalsTable",
                new TableProps
                {
                    TableName = "Goals",
                    BillingMode = BillingMode.PAY_PER_REQUEST,
                    PartitionKey = new Attribute
                    {
                        Name = "uid",
                        Type = AttributeType.STRING
                    },
                });
        }

        public Table GoalsTable { get; set; }
    }
}
