using System.Text.Json;

namespace Smartalert.Api.Controllers.Exceptions
{
    public class ErrorDetails
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
