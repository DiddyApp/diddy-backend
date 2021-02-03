using System;
using Amazon.CDK.AWS.APIGateway;

namespace Infrastructure.Utils
{
    public class CognitoAuthorizer : IAuthorizer
    {
        public string AuthorizerId { get; set; }
    }
}
