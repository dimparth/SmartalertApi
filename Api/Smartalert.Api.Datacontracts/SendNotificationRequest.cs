using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    public class SendNotificationRequest
    {
        [DataMember(Name ="category")]
        public string? Category { get; set; }
        [DataMember(Name ="locality")]
        public string? Locality { get; set; }
    }
}
