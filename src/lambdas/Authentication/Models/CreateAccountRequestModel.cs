using System;
namespace Authentication.Models
{
    public class CreateAccountRequestModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
