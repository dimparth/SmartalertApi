using Encryption;
using Microsoft.Extensions.Logging;
using Smartalert.Api.Datacontracts;
using Smartalert.Api.Interfaces;
using Smartalert.Api.Interfaces.Repositories;
using System.Text.Json;

namespace Smartalert.Api.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IEncryptionService encryptionService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _logger = logger;
        }

        public async Task<string?> AddUser(UserRequest user)
        {
            if (user == null)
            {
                _logger.LogError("null");
                throw new ArgumentNullException(nameof(user));
            }
            if (!(user.Username.ValidateUserInput() && user.Pass.ValidateUserInput()))
            {
                _logger.LogError("invalid");
                throw new Exception("username/password is invalid");
            }
            user.Pass = _encryptionService.Encrypt(user.Pass);
            var newuser = user.ToUserDataModel();
            var result = await _userRepository.Add(newuser);
            if (result > 0)
            {
                _logger.LogInformation("added {user}", newuser);
                return newuser?.Username;
            }
            else
            {
                _logger.LogError("invalid");
                throw new Exception("error");
            }

        }

        public async Task<int> UpdateUserRole(string? username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            var result = await _userRepository.Update(username);
            return result;
        }

        public async Task<UsersListResponse> GetAllUsers()
        {
            var response = new UsersListResponse { Users = (await _userRepository.GetAll()).ToUserResponse(_encryptionService) };
            _logger.LogInformation("GetAllUsers response: {response}", JsonSerializer.Serialize(response));
            return response;
        }
    }
}
