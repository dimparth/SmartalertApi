using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    [DataContract(Name = "user")]
    public class UserResponse
    {
        [DataMember(Name = "username")]
        public string? Username { get; set; }
        [DataMember(Name = "password")]
        public string? Pass { get; set; }
        [DataMember(Name = "role")]
        public string? Role { get; set; }
    }
}
