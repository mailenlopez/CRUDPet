using Application.Repositories;
using Dapper;
using Domain.Common;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AuditableEntity
    {
        private readonly string _tableName;
        protected string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Sqlite")!;
            _tableName = $"{typeof(T).Name}s";
        }

        public async Task<int> AddAsync(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
            var query = $"INSERT INTO {_tableName} ({stringOfColumns}) VALUES ({stringOfParameters})";
            using (SqliteConnection  conn = new SqliteConnection (_connectionString))
            {
                conn.Open();
                var result = await conn.ExecuteAsync(query, entity);
                return result;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqliteConnection  conn = new(_connectionString))
            {
                conn.Open();
                await conn.ExecuteAsync($"DELETE FROM {_tableName} WHERE [Id] = @Id", new { Id = id });
            }
        }

        public async Task DeleteAllAsync()
        {
            using (SqliteConnection conn = new(_connectionString))
            {
                conn.Open();
                await conn.ExecuteAsync($"DELETE FROM {_tableName}");
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (SqliteConnection  conn = new(_connectionString))
            {
                conn.Open();
                var data = await conn.QueryAsync<T>($"SELECT * FROM {_tableName}");
                return data;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (SqliteConnection  conn = new(_connectionString))
            {
                conn.Open();
                var data = await conn.QueryAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
                return data.FirstOrDefault()!;
            }
        }

        public async Task<IEnumerable<T>> Query(string where)
        {
            var query = $"SELECT * FROM {_tableName} ";

            if (!string.IsNullOrWhiteSpace(where))
                query += where;

            using (SqliteConnection  conn = new(_connectionString))
            {
                conn.Open();
                var data = await conn.QueryAsync<T>(query);
                return data;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"UPDATE {_tableName} SET {stringOfColumns} WHERE Id = @Id";

            using (SqliteConnection  conn = new(_connectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(query, entity);
            }
        }
        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
