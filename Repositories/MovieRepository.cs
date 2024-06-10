using Dapper;
using MovieMania.Models.Database;
using MovieMania.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MovieMania.Repositories
{
    public class MovieRepository : BaseRepository<Movie>,IMovieRepository
    {
        private readonly string _connectionString;
        public MovieRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {
            _connectionString = connectionString.Value.IMDBDatabaseConnectionString;
        }
        public async Task<List<Movie>> GetAsync()
        {
            string query = @"
SELECT [Id]
	,[Name]
	,[YearOfRelease]
	,[Plot]
	,[ProducerId]
	,[CoverImage]
FROM Foundation.Movies;";
            return await GetAsync(query);
        }

        public async Task<Movie> GetAsync(int id)
        {
            string query = @"
SELECT [Id]
	,[Name]
	,[YearOfRelease]
	,[Plot]
	,[ProducerId]
	,[CoverImage]
FROM Foundation.Movies
WHERE Id = @id;";
            return await GetAsync(query, new {id});
        }

        public async Task<int> CreateAsync(Movie movie, List<int> actorIds, List<int> genreIds)
        {
            string uspName = @"usp_AddMovie";
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@Name", movie.Name);
                parameters.Add("@YearOfRelease", movie.YearOfRelease);
                parameters.Add("@Plot", movie.Plot);
                parameters.Add("@CoverImage", movie.CoverImage);
                parameters.Add("@ProducerId", movie.ProducerId);
                parameters.Add("@GenresIds", string.Join(",", genreIds));
                parameters.Add("@ActorsIds", string.Join(",", actorIds));
                parameters.Add("@MovieId", dbType: DbType.Int32, direction: ParameterDirection.Output); // Output parameter

                await conn.ExecuteAsync(
                    uspName,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                int newId = parameters.Get<int>("@MovieId"); // Retrieve output parameter value
                await conn.CloseAsync();
                return newId;
            }
        }


        public async Task<int> UpdateAsync(Movie movie, List<int> actorIds, List<int> genreIds)
        {
            var parameters = new
            {
                movie.Id,
                movie.Name,
                movie.YearOfRelease,
                movie.Plot,
                movie.CoverImage,
                movie.ProducerId,
                GenresIds = string.Join(",", genreIds),
                ActorsIds = string.Join(",", actorIds)
            };

            return await ExecuteStoredProcedureAsync("usp_UpdateMovie", parameters);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteStoredProcedureAsync("usp_DeleteMovie", new { Id = id });
        }

    }
}
