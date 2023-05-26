using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    [DataContract(Name = "dangerInformation")]
    public class DangerResponse
    {
        [DataMember(Name = "user")]
        public string? Username { get; set; }
        [DataMember(Name = "comments")]
        public string? Comments { get; set; }
        [DataMember(Name = "latitude")]
        public string? Latitude { get; set; }
        [DataMember(Name = "longitude")]
        public string? Longitude { get; set; }
        [DataMember(Name = "dangerTimestamp")]
        public string? DangerTimestamp { get; set; }
        [DataMember(Name = "imagePath")]
        public string? ImagePath { get; set; }
        [DataMember(Name = "dangerStatus")]
        public string? DangerStatus { get; set; } = "REVIEW";
        [DataMember(Name = "category")]
        public string? Category { get; set; }
        [DataMember(Name = "locality")]
        public string? Locality { get; set; }
    }
}
