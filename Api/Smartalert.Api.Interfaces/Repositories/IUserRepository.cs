using Smartalert.Api.Models;

namespace Smartalert.Api.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> FindUser(string? username);
        Task<User> FindUserAsync(string? username,string? password);
    }
}
