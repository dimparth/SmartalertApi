using Smartalert.Api.Datacontracts;

namespace Smartalert.Api.Interfaces
{
    public interface IUserService
    {
        Task<string?> AddUser(UserRequest user);
        Task<int> UpdateUserRole(string? username);
        Task<UsersListResponse> GetAllUsers();
    }
}
