using Microsoft.Extensions.DependencyInjection;

namespace MySqlConnectionFactory
{
    public static class DbConnectionFactoryDI
    {
        public static IServiceCollection AddDbFactory(this IServiceCollection collection, IDictionary<string?, string?> connections)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            return collection.AddSingleton<IDbConnectionFactory, DbConnectionFactory>(factory => new DbConnectionFactory(connections));
        }
    }
}
