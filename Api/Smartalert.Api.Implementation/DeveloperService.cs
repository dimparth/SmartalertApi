using Microsoft.Extensions.Logging;
using Smartalert.Api.Datacontracts;
using Smartalert.Api.Interfaces;
using System.Text.Json;

namespace Smartalert.Api.Implementation
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IUserService _userService;
        private readonly IDangerService _dangerService;
        private readonly ILogger<DeveloperService> _logger;

        public DeveloperService(IUserService userService, IDangerService dangerService, ILogger<DeveloperService> logger)
        {
            _userService = userService;
            _dangerService = dangerService;
            _logger = logger;
        }

        public async Task<string> AddCriticals()
        {
            IList<string?> incidentsAdded = new List<string?>();
            var random = new Random();
            for (int i=1; i<=10; i++)
            {
                var incident = await _dangerService.Add(new DangerResponse
                {
                    Username = $"user{i}",
                    Comments = "samplecomment",
                    Latitude = $"{38 + random.NextDouble()}",
                    Longitude = $"{21 + random.NextDouble()}",
                    DangerTimestamp = DateTime.Now.ToString("yyyy-MM-dd"),
                    ImagePath = "flooddrain.png",
                    DangerStatus = "REVIEW",
                    Category = "flood",
                    Locality = "Karditsa"
                });
                incidentsAdded.Add(incident.ToString());
            }
            var totalIncidentsAdded = JsonSerializer.Serialize(incidentsAdded.Count);
            _logger.LogInformation("Added {totalIncidentsAdded} incidents",totalIncidentsAdded);
            return totalIncidentsAdded;
        }

        public async Task<string> AddUsers()
        {
            IList<string?> usersAdded = new List<string?>();
            var firstAdmin = await _userService.AddUser(new UserRequest { Username = "dimitris", Pass = "123", Role = "admin" });
            usersAdded.Add(firstAdmin);
            var secondAdmin = await _userService.AddUser(new UserRequest { Username = "giorgos", Pass = "123", Role = "admin" });
            usersAdded.Add(secondAdmin);
            for (int i = 1; i <= 10; i++)
            {
                var user = await _userService.AddUser(new UserRequest { Username = $"user{i}", Pass = "123", Role = "user", FcmToken = "fuiT-7s4T2OCTRPtLZKMfG:APA91bFdNlnt3NL4fsGDI7w99HfAiDtBd5XuqikIHzpMdHd1a-_3rE1pTn7r0XWE6s4NinMwbeMxcXcQkTvOrU0bWKnMVWZSqF9vQ0MRuMuhTzLBUKlnoE20saElemefKA-IT-sHe1s3" });
                usersAdded.Add(user);
            }
            var usersTotal = JsonSerializer.Serialize(usersAdded);
            _logger.LogInformation("Added sample users to database {usersTotal}",usersTotal);
            return usersTotal;
        }
    }
}
