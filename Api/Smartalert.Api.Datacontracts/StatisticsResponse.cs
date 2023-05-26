using System.Runtime.Serialization;

namespace Smartalert.Api.Datacontracts
{
    public class StatisticsResponse
    {
        [DataMember(Name = "totalCount")]
        public string? TotalCount { get; set; }
        [DataMember(Name ="usersCount")]
        public string? UsersCount { get; set; }
        [DataMember(Name = "incidentsCategories")]
        public IList<IncidentsCategoryResponse> IncidentsCategories { get; set; } = new List<IncidentsCategoryResponse>();
    }
}
