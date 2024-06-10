using Dapper;
using MovieMania.Models.Database;
using MovieMania.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMania.Repositories
{
    public class GenreRepository : BaseRepository<Genre> , IGenreRepository
    {

        private readonly string _connectionString;
        public GenreRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {
            _connectionString = connectionString.Value.IMDBDatabaseConnectionString;
        }

        public async Task<int> CreateAsync(Genre genre)
        {
            string query = @"
INSERT INTO Foundation.Genres([Name]) 
                             VALUES(@Name);";

            return await CreateAsync(query, genre);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteStoredProcedureAsync("usp_DeleteGenre", new { Id = id });
        }

        public async Task<List<Genre>> GetAsync()
        {
            string query = @"
SELECT [Id]
	,[Name]
FROM Foundation.Genres";
            return await GetAsync(query);
        }

        public async Task<Genre> GetAsync(int id)
        {
            string query = @"
SELECT [Id]
	,[Name]
FROM Foundation.Genres
WHERE Id = @id;";
            return await GetAsync(query, new { id });
        }

        public async Task<int> UpdateAsync(Genre genre)
        {
            string query = @"
UPDATE Foundation.Genres
SET [Name] = @Name
	,[UpdatedAt] = GETDATE()
WHERE [Id] = @Id;";
            return await UpdateAsync(query, genre);
        }

        public async Task<List<int>> GetGenresIdsByMovieIdAsync(int movieId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"
SELECT GenreId 
FROM Foundation.Genre_Movies 
WHERE MovieId = @movieId";

                    var parameters = new { movieId };

                    var genreIds = await conn.QueryAsync<int>(query, parameters);

                    return genreIds.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}