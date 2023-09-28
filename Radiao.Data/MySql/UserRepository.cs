using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using System.Text;

namespace Radiao.Data.MySql
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySql")!;
        }

        public async Task<User> Create(User user)
        {
            var query = new StringBuilder();
            query.Append("insert into users (Email, Password, Name) ");
            query.Append("values (@Email, @Password, @Name) ");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), user);
            
            return user;
        }

        public async Task Delete(int id)
        {
            var query = new StringBuilder();
            query.Append("update users set IsActive = false ");
            query.Append("where Id = @Id ");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), new { id });
        }

        public async Task<User?> Get(int id)
        {
            var query = new StringBuilder();
            query.Append("select * from users ");
            query.Append("where Id = @Id ");

            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(query.ToString(), new { id });
        }

        public async Task<List<User>> GetAll()
        {
            var query = new StringBuilder();
            query.Append("select * from users");

            using var connection = new MySqlConnection(_connectionString);
            return (List<User>)await connection.QueryAsync<User>(query.ToString());
        }

        public async Task<User?> GetByEmail(string email)
        {
            var query = new StringBuilder();
            query.Append("select * from users ");
            query.Append("where Email = @Email ");

            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(query.ToString(), new { email });
        }

        public async Task<User> Update(User user)
        {
            var query = new StringBuilder();
            query.Append("update users set Email = @Email, Name = @Name ");
            query.Append("where Id = @Id");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), user);

            return user;
        }
    }
}
