using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;

namespace Infrastructure.Authentication
{
    public class DynamoDbResources : Construct
    {
        public DynamoDbResources(Construct scope, string id) : base(scope, $"{id}-DynamoDb")
        {
            UsersTable = new Table(scope, "UsersTable",
               new TableProps
               {
                   TableName = "Users",
                   BillingMode = BillingMode.PAY_PER_REQUEST, // will probably be changes to Provisioned in production
                    PartitionKey = new Attribute
                   {
                       Name = "id",
                       Type = AttributeType.STRING
                   },
               });
        }

        public Table UsersTable { get; private set; }
    }
}
