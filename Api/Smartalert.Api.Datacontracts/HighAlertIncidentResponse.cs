using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    [DataContract(Name = "criticalDangers")]
    public class HighAlertIncidentResponse
    {
        [DataMember(Name ="category")]
        public string? Category { get; set; }
        [DataMember(Name ="locality")]
        public string? Locality { get; set; }
        [DataMember(Name ="count")]
        public string? DangerCount { get; set; }
    }
}
