using System;
namespace Authentication.Models
{
    public class UserAuthResponseModel
    {
        public string UserId { get; set; }

        public string IdToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
