using Application.Repositories;
using Dapper;
using Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var query = $"SELECT * FROM Users WHERE Email = @email ";

            var parameters = new DynamicParameters();
            parameters.Add("@email", email, DbType.String);

            using (SqliteConnection conn = new(_connectionString))
            {
                conn.Open();
                var data = await conn.QueryFirstOrDefaultAsync<User>(query, parameters);
                return data;
            }
        }
    }
}
