namespace Smartalert.Api.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<int> Add(T entity);
        Task<int> Update(string? id);
        Task<IList<T>> GetAll();
    }
}
