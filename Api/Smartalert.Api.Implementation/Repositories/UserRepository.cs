using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnectionFactory;
using Smartalert.Api.Interfaces.Repositories;
using Smartalert.Api.Models;
using System.Data;

namespace Smartalert.Api.Implementation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;
        private readonly ConnectionStringOptions _connectionStringOptions;
        public UserRepository(IDbConnectionFactory connectionFactory, IOptions<ConnectionStringOptions> connectionStringOptions)
        {
            _connectionStringOptions = connectionStringOptions.Value;
            _connection = connectionFactory.MySqlConnection(_connectionStringOptions.FirstOrDefault().Key);
        }

        public async Task<int> Add(User user)
        {
            var query = "INSERT INTO Users(Username,Pass,Role,FcmToken) VALUES(@Username,@Pass,@Role,@FcmToken)";
            var result = await _connection.ExecuteAsync(query, user);
            return result;

        }

        public async Task<IList<User>> GetAll()
        {
            var query = "SELECT * FROM Users";
            var result = await _connection.QueryAsync<User>(query);
            return result.ToList();
        }
        public async Task<bool> FindUser(string? username)
        {
            var query = "SELECT 1 FROM Users WHERE Username = @Username";
            var result = await _connection.QueryFirstOrDefaultAsync<int>(query, new { Username = username });
            return result == 1;
        }
        public async Task<int> Update(string? userId)
        {
            var query = "UPDATE Users SET Role = 'admin' WHERE Username = @Username";
            var result = await _connection.QueryFirstOrDefaultAsync<int>(query, new {Username = userId});
            return result;
        }

        public async Task<User> FindUserAsync(string? username, string? password)
        {
            var query = "SELECT Username,Role FROM Users WHERE Username=@Username AND Pass = @Pass";
            var result = await _connection.QueryFirstOrDefaultAsync<User>(query, new {Username = username, Pass = password});
            return result;
        }
    }
}
