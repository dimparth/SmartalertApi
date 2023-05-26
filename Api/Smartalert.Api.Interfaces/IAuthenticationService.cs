using Smartalert.Api.Datacontracts;

namespace Smartalert.Api.Interfaces
{
    public interface IAuthenticationService
    {
        Task<JWTResponse> Authenticate(AuthenticationRequest user);
    }
}
