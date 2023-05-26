using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    [DataContract(Name = "usersList")]
    public class UsersListResponse
    {
        [DataMember(Name = "users")]
        public IList<UserResponse> Users { get; set; } = new List<UserResponse>();
    }
}
