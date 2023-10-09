using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using System.Text;

namespace Radiao.Data.MySql
{
    public class TagRepository : ITagRepository
    {
        private readonly string _connectionString;

        public TagRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySql")!;
        }

        public async Task<Tag> Create(Tag tag)
        {
            var query = new StringBuilder();
            query.Append("insert into tags (Id, Name) ");
            query.Append("values (@Id, @Name)");

            tag.NewId();

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), tag);

            return await connection
                .QueryFirstOrDefaultAsync<Tag>("select * from tags where Id = @id", new { id = tag.Id });
        }

        public async Task Delete(Guid id)
        {
            var query = "update tags set IsActive = 0 where Id = @id";

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), new { id });
        }

        public async Task<List<Tag>> GetAll()
        {
            var query = "select * from tags where IsActive = 1";

            using var connection = new MySqlConnection(_connectionString);
            return (List<Tag>)await connection.QueryAsync<Tag>(query.ToString());
        }

        public async Task<Tag> GetById(Guid id)
        {
            var query = new StringBuilder();
            query.Append("select * from tags ");
            query.Append("where Id = @Id ");

            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Tag>(query.ToString(), new { id });
        }

        public async Task<Tag> GetByName(string name)
        {
            var query = new StringBuilder();
            query.Append("select * from tags ");
            query.Append("where Name = @name ");

            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Tag>(query.ToString(), new { name });
        }

        public async Task<Tag> Update(Tag tag)
        {
            var query = new StringBuilder();
            query.Append("update tags set ");
            query.Append(" Name = @Name, ");
            query.Append(" IsActive = 1 "); // IsActive will be fixed for now
            query.Append("where Id = @Id");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), tag);

            return tag;
        }
    }
}
