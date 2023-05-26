using Smartalert.Api.Models;

namespace Smartalert.Api.Interfaces.Repositories
{
    public interface IDangerRepository : IRepository<DangerInformation>
    {
        Task<IList<HighAlertIncident>> GetCriticalIncidents();
        Task<LatitudeLongitude> GetLatestCoords(string? locality, string? category);
        Task<IList<UsersInRange>> GetCoordsInRange(string? latitude, string? longitude, string? category);
        Task<IList<IncidentsByCategory>> GetIncidentCountByCategory();
        Task<string?> GetIncidentsCount();
        Task<string?> GetUsersCount();
    }
}
