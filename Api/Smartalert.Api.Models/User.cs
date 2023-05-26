namespace Smartalert.Api.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? Pass { get; set; }
        public string? Role { get; set; }
        public string? FcmToken { get; set; }
    }
}
