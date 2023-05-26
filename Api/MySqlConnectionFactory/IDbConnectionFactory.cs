using System.Data;

namespace MySqlConnectionFactory
{
    public interface IDbConnectionFactory
    {
        IDbConnection MySqlConnection(string? connectionName);
    }
}
