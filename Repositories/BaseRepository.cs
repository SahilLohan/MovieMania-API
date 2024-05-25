using Dapper;
using MovieMania.Models.Database;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMania.Repositories
{
    public class BaseRepository<T> where T : class
    {
        private string _connectionString;
        public BaseRepository(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.IMDBDatabaseConnectionString;
        }

        public async Task<int> CreateAsync(string query,T entity)
        {
            SqlConnection connection = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    connection = conn;
                    await conn.OpenAsync();
                    int newId = await conn.ExecuteScalarAsync<int>(query + "SELECT CAST(SCOPE_IDENTITY() as int);", entity);
                    await conn.CloseAsync(); // or conn.Dispose();
                    return newId;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync(); // or conn.Dispose();
            }
        }

        public async Task<List<T>> GetAsync(string query)
        {
            SqlConnection connection = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    connection = conn;
                    await conn.OpenAsync();
                    var entities = await conn.QueryAsync<T>(query);
                    await conn.CloseAsync(); // or conn.Dispose();
                    return entities.AsList();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync(); // or conn.Dispose();
            }
        }

        public async Task<T> GetAsync(string query,Object parameters)
        {
            SqlConnection connection = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    connection = conn;
                    await conn.OpenAsync();
                    var entity = await conn.QueryFirstOrDefaultAsync<T>(query, parameters);
                    await conn.CloseAsync(); // or conn.Dispose();
                    return entity;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync(); // or conn.Dispose();
            }
        }

        public async Task<int> UpdateAsync(string query,T entity)
        {
            SqlConnection connection = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    connection = conn;
                    await conn.OpenAsync();
                    int rowsAffected = await conn.ExecuteAsync(query, entity);
                    return rowsAffected;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }
        }

        public async Task<int> DeleteAsync(string query, int id)
        {
            SqlConnection connection = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    connection = conn;
                    await conn.OpenAsync();
                    var parameters = new
                    {
                        @Id = id
                    };
                    int rowsAffected = await conn.ExecuteAsync(query, parameters);
                    return rowsAffected;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }
        }

        public async Task<int> ExecuteStoredProcedureAsync(string spName,Object parameters)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    int rowsAffected = await conn.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
