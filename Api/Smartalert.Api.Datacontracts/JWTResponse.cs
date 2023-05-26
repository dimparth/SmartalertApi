using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    public class JWTResponse
    {
        [DataMember(Name = "token")]
        public string? Token { get; set; }
        [DataMember(Name ="username")]
        public string? Username { get; set; }
        [DataMember(Name = "role")]
        public string? Role { get; set; }
    }
}
