using System;

namespace Domain.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; internal set; }
    }
}
