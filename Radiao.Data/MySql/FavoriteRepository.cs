using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using System.Text;

namespace Radiao.Data.MySql
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly string _connectionString;

        public FavoriteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySql")!;
        }

        public async Task<Favorite> Create(Favorite favorite)
        {
            var query = new StringBuilder();
            query.Append("insert into favorites (UserId, StationId) ");
            query.Append("values (@UserId, @StationId)");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), favorite);

            return favorite;
        }

        public async Task Delete(int id)
        {
            var query = new StringBuilder();
            query.Append("delete from favorites ");
            query.Append("where Id = @id");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), new { id });
        }

        public async Task<Favorite?> Get(int id)
        {
            var query = new StringBuilder();
            query.Append("select * from favorites ");
            query.Append("where Id = @id");

            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Favorite>(query.ToString(), new { id });
        }

        public async Task<List<Favorite>> GetAll(int userId)
        {
            var query = new StringBuilder();
            query.Append("select * from favorites ");
            query.Append("where UserId = @userId");

            using var connection = new MySqlConnection(_connectionString);
            return (List<Favorite>)await connection.QueryAsync<Favorite>(query.ToString(), new { userId });
        }

        public async Task<Favorite?> GetByUserAndStation(int userId, Guid stationId)
        {
            var query = new StringBuilder();
            query.Append("select * from favorites ");
            query.Append("where UserId = @userId and StationId = @stationId");

            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Favorite>(query.ToString(), new
            { 
                userId,
                stationId
            });
        }
    }
}
