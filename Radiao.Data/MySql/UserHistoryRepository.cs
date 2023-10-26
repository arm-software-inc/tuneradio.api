using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using System.Text;

namespace Radiao.Data.MySql
{
    public class UserHistoryRepository : IUserHistoryRepository
    {
        private readonly string _connectionString;

        public UserHistoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySql")!;
        }

        public async Task<UserHistory> Create(UserHistory userHistory)
        {
            var query = new StringBuilder();
            query.Append("insert into user_history (Id, UserId, StationId) ");
            query.Append("values (@Id, @UserId, @StationId)");

            userHistory.NewId();

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), userHistory);

            return await connection
                .QueryFirstOrDefaultAsync<UserHistory>("select * from user_history where Id = @id", new { id = userHistory.Id });
        }

        public async Task Delete(Guid id)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync("delete from user_history where Id = @id", new { id });
        }

        public async Task DeleteByUserId(Guid userId)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync("delete from user_history where UserId = @userId", new { userId });
        }

        public async Task<UserHistory> Get(Guid id)
        {
            using var connection = new MySqlConnection(_connectionString);
            return await connection
                .QueryFirstOrDefaultAsync<UserHistory>("select * from user_history where Id = @id", new { id });
        }

        public async Task<List<UserHistory>> GetByUserId(Guid userId)
        {
            using var connection = new MySqlConnection(_connectionString);
            return (List<UserHistory>)await connection
                .QueryAsync<UserHistory>("select * from user_history where UserId = @userId limit 10", new { userId });
        }
    }
}
