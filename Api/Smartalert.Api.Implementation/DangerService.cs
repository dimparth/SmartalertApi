using Microsoft.Extensions.Logging;
using Smartalert.Api.Datacontracts;
using Smartalert.Api.Interfaces;
using Smartalert.Api.Interfaces.Repositories;
using Smartalert.Api.Models;
using System.Text.Json;

namespace Smartalert.Api.Implementation
{
    public class DangerService : IDangerService
    {
        private readonly IDangerRepository _dangerRepository;
        private readonly ILogger<DangerService> _logger;

        public DangerService(IDangerRepository dangerRepository, ILogger<DangerService> logger)
        {
            _dangerRepository = dangerRepository;
            _logger = logger;
        }

        public async Task<int> Add(DangerResponse dangerInformation)
        {
            var response = await _dangerRepository.Add(dangerInformation.ToDangerDataModel());
            _logger.LogInformation("added critical incident");
            return response;
        }
        public async Task<int> Update(string? dangerId)
        {
            var response = await _dangerRepository.Update(dangerId);
            _logger.LogInformation("updated {dangerId} status", dangerId);
            return response;
        }
        public async Task<DangerListResponse> GetAll()
        {
            var response = new DangerListResponse { Users = (await _dangerRepository.GetAll()).ToDangerList() };
            _logger.LogInformation("All incidents: {response}", JsonSerializer.Serialize(response));
            return response;
        }

        public async Task<IList<HighAlertIncidentResponse>> GetCriticalSitutations()
        {
            var response = await _dangerRepository.GetCriticalIncidents();
            _logger.LogInformation("All critical incidents: {response}", JsonSerializer.Serialize(response));
            return response.ToIncidentResponse();
        }
        public async Task<IList<UsersInRange>> GetLocationsToNotify(string? locality, string? category)
        {
            var centerLocation = await _dangerRepository.GetLatestCoords(locality, category);
            var locations = await _dangerRepository.GetCoordsInRange(centerLocation.Latitude,centerLocation.Longitude,category);
            _logger.LogInformation("All locations to notify: {response}", JsonSerializer.Serialize(locations));
            return locations;
        }
        public async Task<StatisticsResponse> IncidentsCategoryResponse()
        {
            var totalIncidents = await _dangerRepository.GetIncidentsCount();
            var incidentsByCategory = await _dangerRepository.GetIncidentCountByCategory();
            var totalUsers = await _dangerRepository.GetUsersCount();
            var response = incidentsByCategory.ToStatisticsResponse(totalIncidents,totalUsers);
            return response;
        }
    }
}
