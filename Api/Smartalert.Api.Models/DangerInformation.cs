using System.Runtime.Serialization;

namespace Smartalert.Api.Models
{
    public class DangerInformation
    {
        public string? Username { get; set; }
        public string? Comments { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? DangerTimestamp { get; set; } = DateTime.Now.ToString();
        public string? ImagePath { get; set; }
        public string? DangerStatus { get; set; } = "REVIEW";
        public string? Category { get; set; }
        public string? Locality { get; set; }
    }
}
