using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    public class AuthenticationRequest
    {
        [DataMember(Name ="username")]
        public string? Username { get; set; }
        [DataMember(Name ="pass")]
        public string? Pass { get; set; }
    }
}
