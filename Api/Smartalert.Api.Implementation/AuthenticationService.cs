using Encryption;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Smartalert.Api.Datacontracts;
using Smartalert.Api.Interfaces;
using Smartalert.Api.Interfaces.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Smartalert.Api.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTOptions _jwtOptions;
        private readonly IEncryptionService _encryptionService;

        public AuthenticationService(IUserRepository userRepository, IOptions<JWTOptions> jwtOptions, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _jwtOptions = jwtOptions.Value;
            _encryptionService = encryptionService;
        }

        public async Task<JWTResponse> Authenticate(AuthenticationRequest user)
        {
            if (!(user.Username.ValidateUserInput() && user.Pass.ValidateUserInput()))
            {
                throw new Exception("username/password is invalid");
            }
            var userExists = await _userRepository.FindUser(user.Username);
            if (!userExists)
            {
                throw new Exception($"user {user.Username} not found");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tk = _jwtOptions.Key?.Encode();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, user.Username ?? throw new NullReferenceException("error claiming username"))
              }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tk), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var authenticatedUser = await _userRepository.FindUserAsync(user?.Username, _encryptionService.Encrypt(user?.Pass));
            return new JWTResponse
            {
                Token = tokenHandler.WriteToken(token),
                Username = authenticatedUser.Username,
                Role = authenticatedUser.Role
            };
        }
    }
}
