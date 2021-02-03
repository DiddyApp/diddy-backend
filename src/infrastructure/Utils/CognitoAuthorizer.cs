using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;

namespace Infrastructure.Utils
{
    public class CognitoAuthorizer : CfnAuthorizer, IAuthorizer
    {
        public CognitoAuthorizer(Construct scope, string id, CfnAuthorizerProps props)
            :base(scope, id, props)
        {
            AuthorizerId = Ref;
        }

        public string AuthorizerId { get; private set; }
        public AuthorizationType AuthorizationType { get { return AuthorizationType.COGNITO; } }
    }
}
