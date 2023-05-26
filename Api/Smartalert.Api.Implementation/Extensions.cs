using Encryption;
using Smartalert.Api.Datacontracts;
using Smartalert.Api.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace Smartalert.Api.Implementation
{
    internal static class Extensions
    {
        internal static byte[] Encode(this string tokenKey)
        {
            if (string.IsNullOrEmpty(tokenKey))
            {
                throw new ArgumentNullException(nameof(tokenKey));
            }
            return Encoding.UTF8.GetBytes(tokenKey);
        }
        internal static DangerInformation ToDangerDataModel(this DangerResponse dangerResponse)
        {
            DangerInformation dangerInformation = new()
            {
                Username = dangerResponse.Username,
                Comments = dangerResponse.Comments,
                Latitude = dangerResponse.Latitude,
                Longitude = dangerResponse.Longitude,
                DangerTimestamp = dangerResponse.DangerTimestamp,
                ImagePath = dangerResponse.ImagePath,
                DangerStatus = dangerResponse.DangerStatus,
                Category = dangerResponse.Category,
                Locality = dangerResponse.Locality
            };
            return dangerInformation;
        }
        internal static IList<DangerResponse> ToDangerList(this IList<DangerInformation> dangerInformation)
        {
            List<DangerResponse> dangerListResponse = new();
            foreach (var danger in dangerInformation)
            {
                var dangerResponse = new DangerResponse
                {
                    Username = danger.Username,
                    Comments = danger.Comments,
                    Latitude = danger.Latitude,
                    Longitude = danger.Longitude,
                    DangerTimestamp = danger.DangerTimestamp,
                    ImagePath = danger.ImagePath,
                    DangerStatus = danger.DangerStatus,
                    Category = danger.Category,
                    Locality = danger.Locality
                };
                dangerListResponse.Add(dangerResponse);
            }
            return dangerListResponse;
        }
        internal static StatisticsResponse ToStatisticsResponse(this IList<IncidentsByCategory> incidentsByCategory, string? totalCount, string? usersCount)
        {
            return new StatisticsResponse { TotalCount = totalCount, UsersCount = usersCount, IncidentsCategories = incidentsByCategory.ToIncidentsCategoryResponse() };
        }
        internal static IList<IncidentsCategoryResponse> ToIncidentsCategoryResponse(this IList<IncidentsByCategory> incidentsByCategory)
        {
            List<IncidentsCategoryResponse> incidentsCategoryResponses = new();
            foreach (var incident in incidentsByCategory)
            {
                incidentsCategoryResponses.Add(new IncidentsCategoryResponse { Count = incident.Count, Category = incident.Category, Day = incident.DangerDay });
            }
            return incidentsCategoryResponses;
        }
        internal static IList<HighAlertIncidentResponse> ToIncidentResponse(this IList<HighAlertIncident> incidents)
        {
            List<HighAlertIncidentResponse> incidentResponseList = new();
            foreach (var incident in incidents)
            {
                var incidentResponse = new HighAlertIncidentResponse
                {
                    Category = incident.Category,
                    Locality = incident.Locality,
                    DangerCount = incident.DangerCount
                };
                incidentResponseList.Add(incidentResponse);
            }
            return incidentResponseList;
        }
        internal static double ToDouble(this string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }
            return Convert.ToDouble(input);
        }
        internal static User ToUserDataModel(this UserRequest userRequest)
        {
            User user = new()
            {
                Username = userRequest.Username,
                Pass = userRequest.Pass,
                Role = userRequest.Role,
                FcmToken = userRequest.FcmToken
            };
            return user;
        }
        internal static bool ValidateUserInput(this string? userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                return false;
            }
            Regex regex = new(@"^[A-Za-z0-9@._]+$");
            var isMatch = regex.IsMatch(userInput);
            return isMatch;
        }
        internal static IList<UserResponse> ToUserResponse(this IList<User> users,IEncryptionService encryptionService)
        {
            var list = new List<UserResponse>();
            foreach (var user in users)
            {
                var response = new UserResponse
                {
                    Username = user.Username,
                    Pass = encryptionService.Decrypt(user.Pass),
                    Role = user.Role
                };
                list.Add(response);
            }
            return list;
        }
    }
}
