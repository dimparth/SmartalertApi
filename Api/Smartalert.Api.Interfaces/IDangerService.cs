using Smartalert.Api.Datacontracts;
using Smartalert.Api.Models;

namespace Smartalert.Api.Interfaces
{
    public interface IDangerService
    {
        Task<int> Add(DangerResponse dangerInformation);
        Task<int> Update(string? dangerId);
        Task<DangerListResponse> GetAll();
        Task<IList<HighAlertIncidentResponse>> GetCriticalSitutations();
        Task<IList<UsersInRange>> GetLocationsToNotify(string? locality, string? category);
        Task<StatisticsResponse> IncidentsCategoryResponse();
    }
}
