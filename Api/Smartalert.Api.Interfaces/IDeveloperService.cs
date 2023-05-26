namespace Smartalert.Api.Interfaces
{
    public interface IDeveloperService
    {
        Task<string> AddUsers();
        Task<string> AddCriticals();
    }
}
