using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    public class DangerListResponse
    {
        [DataMember(Name = "dangerInformationList")]
        public IList<DangerResponse> Users { get; set; } = new List<DangerResponse>();
    }
}
