using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    [DataContract(Name = "user")]
    public class UserRequest
    {
        [DataMember(Name = "username")]
        public string? Username { get; set; }
        [DataMember(Name = "password")]
        public string? Pass { get; set; }
        [DataMember(Name = "role")]
        public string? Role { get; set; }
        [DataMember(Name ="fcmToken")]
        public string? FcmToken { get; set; }
    }
}

