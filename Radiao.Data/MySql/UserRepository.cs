﻿using Dapper;
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
            user.NewId();

            var query = new StringBuilder();
            query.Append("insert into users (Id, Email, Password, Name) ");
            query.Append("values (@Id, @Email, @Password, @Name) ");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), user);
            
            return await connection.QueryFirstOrDefaultAsync<User>("select * from users where Id = @id", new { id = user.Id });
        }

        public async Task Delete(Guid id)
        {
            var query = new StringBuilder();
            query.Append("update users set IsActive = false ");
            query.Append("where Id = @Id ");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), new { id });
        }

        public async Task<User?> Get(Guid id)
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

        public async Task UpdatePassword(Guid id, string password)
        {
            var query = new StringBuilder();
            query.Append("update users set Password = @password ");
            query.Append("where Id = @id");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), new { id, password });
        }
    }
}
