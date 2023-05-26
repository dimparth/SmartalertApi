using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    public class IncidentsCategoryResponse
    {
        [DataMember(Name = "count")]
        public string? Count { get; set; }
        [DataMember(Name = "category")]
        public string? Category { get; set; }
        [DataMember(Name = "day")]
        public string? Day { get; set; }
    }
}
