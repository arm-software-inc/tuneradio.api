using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Radiao.Domain.Entities;
using Radiao.Domain.Enums;
using Radiao.Domain.Repositories;
using System.Text;

namespace Radiao.Data.MySql
{
    public class TemplateEmailRepository : ITemplateEmailRepository
    {
        private readonly string _connectionString;

        public TemplateEmailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySql")!;
        }

        public async Task<TemplateEmail> Create(TemplateEmail templateEmail)
        {
            templateEmail.NewId();

            var query = new StringBuilder();
            query.Append("insert into template_emails (Id, Name, Template, TemplateType, EmailSubject) ");
            query.Append("values (@Id, @Name, @Template, @TemplateType, @EmailSubject)");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), templateEmail);

            return templateEmail;
        }

        public async Task Delete(Guid id)
        {
            var query = "delete from template_emails where Id = @id";

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), new { id });
        }

        public async Task<List<TemplateEmail>> GetAll()
        {
            var query = "select * from template_emails";

            using var connection = new MySqlConnection(_connectionString);
            return (List<TemplateEmail>)await connection.QueryAsync<TemplateEmail>(query.ToString());
        }

        public async Task<TemplateEmail?> GetById(Guid id)
        {
            var query = "select * from template_emails where Id = @id";

            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<TemplateEmail>(query.ToString(), new { id });
        }

        public async Task<TemplateEmail?> GetByType(TemplateEmailType templateEmailType)
        {
            var query = "select * from template_emails where IsActive = 1 and TemplateType = @templateEmailType";

            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<TemplateEmail>(query.ToString(), new { templateEmailType });
        }

        public async Task<TemplateEmail> Update(TemplateEmail templateEmail)
        {
            var query = new StringBuilder();
            query.Append("update template_emails set ");
            query.Append(" Name = @Name, ");
            query.Append(" Template = @Template, ");
            query.Append(" EmailSubject = @EmailSubject, ");
            query.Append(" IsActive = @IsActive, ");
            query.Append(" UpdatedAt = now() ");
            query.Append("where Id = @Id");

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(query.ToString(), templateEmail);

            return templateEmail;
        }
    }
}
