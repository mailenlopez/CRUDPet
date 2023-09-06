using Application.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Repositories
{
    public class PetRepository : BaseRepository<Pet>, IPetRepository
    {
        public PetRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<Pet>> GetPetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var query = $"SELECT * FROM Pets WHERE Name = @name ";

            var parameters = new DynamicParameters();
            parameters.Add("name", name, DbType.String);

            using (SqlConnection conn = new(_connectionString))
            {
                conn.Open();
                var data = await conn.QueryAsync<Pet>(query, parameters);
                return data;
            }
        }
    }
}
