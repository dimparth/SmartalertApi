using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnectionFactory;
using Smartalert.Api.Interfaces.Repositories;
using Smartalert.Api.Models;
using System.Data;

namespace Smartalert.Api.Implementation.Repositories
{
    public class DangerRepository : IDangerRepository
    {
        private readonly IDbConnection _connection;
        private readonly ConnectionStringOptions _connectionStringOptions;

        public DangerRepository(IDbConnectionFactory connectionFactory, IOptions<ConnectionStringOptions> connectionStringOptions)
        {
            _connectionStringOptions = connectionStringOptions.Value;
            _connection = connectionFactory.MySqlConnection(_connectionStringOptions.FirstOrDefault().Key);
        }

        public async Task<int> Add(DangerInformation dangerInformation)
        {
            var query = "INSERT INTO DangerInformation(Username, Comments, Latitude,  Longitude, DangerTimestamp, ImagePath, DangerStatus, Category, Locality) " +
                        "VALUES(@Username, @Comments, @Latitude, @Longitude, @DangerTimestamp, @ImagePath, @DangerStatus, @Category, @Locality)";
            var result = await _connection.ExecuteAsync(query, dangerInformation);
            return result;
        }
        public async Task<int> Update(string? dangerId)
        {
            var query = "UPDATE DangerInformation SET DangerStatus = 'CRITICAL' WHERE DangerId = @DangerId";
            var result = await _connection.ExecuteAsync(query, new { DangerId = dangerId });
            return result;
        }
        public async Task<IList<DangerInformation>> GetAll()
        {
            var query = "select * from DangerInformation";
            var result = await _connection.QueryAsync<DangerInformation>(query);
            return result.ToList();
        }

        public async Task<IList<HighAlertIncident>> GetCriticalIncidents()
        {
            var query = "SELECT Category, Locality, COUNT(DangerId) AS DangerCount FROM DangerInformation " +
                        "WHERE DangerTimestamp > (CURRENT_DATE - INTERVAL 1 MONTH) GROUP BY Category, Locality";
            var result = await _connection.QueryAsync<HighAlertIncident>(query);
            return result.ToList();
        }

        public async Task<LatitudeLongitude> GetLatestCoords(string? locality, string? category)
        {
            var query = "SELECT Latitude,Longitude FROM DangerInformation " +
                        "WHERE Locality = @Locality AND Category = @Category AND DangerTimestamp > (CURRENT_DATE - INTERVAL 1 MONTH) ORDER BY DangerId DESC LIMIT 1";
            var result = await _connection.QueryFirstOrDefaultAsync<LatitudeLongitude>(query, new { Locality = locality, Category = category });
            return result;
        }
        public async Task<IList<UsersInRange>> GetCoordsInRange(string? latitude, string? longitude, string? category)
        {
            var query = "SELECT Users.Username, Users.FcmToken, DangerInformation.DangerId, DangerInformation.Category, DangerInformation.Latitude, DangerInformation.Longitude, " +
                        "(6371 * acos(cos(radians(@Latitude)) * cos(radians(DangerInformation.Latitude)) * " +
                        "cos(radians(DangerInformation.Longitude) - radians(@Longitude)) + sin(radians(@Latitude)) * " +
                        "sin(radians(DangerInformation.Latitude)))) AS Distance " +
                        "FROM DangerInformation " +
                        "INNER JOIN Users ON DangerInformation.Username = Users.Username " +
                        " WHERE DangerInformation.Category = @Category HAVING " +
                        "Distance < 25 " +
                        " ORDER BY Distance";
            var result = await _connection.QueryAsync<UsersInRange>(query, new { Latitude = latitude, Longitude = longitude, Category = category });
            return result.ToList();
        }
        public async Task<IList<IncidentsByCategory>> GetIncidentCountByCategory()
        {
            //var query = "SELECT COUNT(DangerId) AS Count, Category FROM DangerInformation WHERE DangerStatus = 'CRITICAL' GROUP BY Category";
            var query = " SELECT COUNT(DangerId) AS Count, Category, CONCAT(EXTRACT(DAY FROM DangerTimestamp),' ',EXTRACT(MONTH FROM DangerTimestamp)) AS DangerDay" +
                        " FROM DangerInformation WHERE DangerStatus = 'CRITICAL' GROUP BY Category, EXTRACT(DAY FROM DangerTimestamp)";
            var result = await _connection.QueryAsync<IncidentsByCategory>(query);
            return result.ToList();
        }
        public async Task<string?> GetIncidentsCount()
        {
            var query = "SELECT Count(DangerId) FROM DangerInformation WHERE DangerStatus = 'CRITICAL'";
            var result = await _connection.QueryFirstOrDefaultAsync<string>(query);
            return result;
        }
        public async Task<string?> GetUsersCount()
        {
            var query = "SELECT COUNT(DISTINCT Username) FROM DangerInformation";
            var result = await _connection.QueryFirstOrDefaultAsync<string?>(query);
            return result;
        }
    }
}
