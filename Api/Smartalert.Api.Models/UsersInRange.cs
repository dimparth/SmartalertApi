namespace Smartalert.Api.Models
{
    public class UsersInRange
    {
        public string? Username{ get; set; }
        public string? FcmToken { get; set; }
        public string? DangerId { get; set; }
        public string? Category { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Distance { get; set; }
    }
}
