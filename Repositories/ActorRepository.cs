using MovieMania.Models.Database;
using MovieMania.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
namespace MovieMania.Repositories
{
    public class ActorRepository : BaseRepository<Actor>,IActorRepository
    {
        private string _connectionString;
        public ActorRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {
            _connectionString = connectionString.Value.IMDBDatabaseConnectionString;
        }

        public async Task<int> CreateAsync(Actor actor)
        {
            string query = @"
INSERT INTO Foundation.Actors (
	[Name]
	,[Gender]
	,[DOB]
	,[Bio]
	)
VALUES (
	@Name
	,@Gender
	,@DOB
	,@Bio
	);";

            return await CreateAsync(query, actor);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteStoredProcedureAsync("usp_DeleteActor", id);   
        }

        public async Task<List<Actor>> GetAsync()
        {
            string query = @"
SELECT [Id]
	,[Name]
	,[Gender]
	,[DOB]
	,[Bio]
FROM Foundation.Actors";
            return await GetAsync(query);
        }

        public async Task<Actor> GetAsync(int id)
        {
            string query = @"
SELECT [Id]
	,[Name]
	,[Gender]
	,[DOB]
	,[Bio]
FROM Foundation.Actors
WHERE [Id] = @Id";
            return await GetAsync(query, new {Id=id});
        }

        public async Task<int> UpdateAsync(Actor actor)
        {
            string query = @"
UPDATE Foundation.Actors
SET [Name] = @Name
	,[Gender] = @Gender
	,[DOB] = @DOB
	,[Bio] = @Bio
	,[UpdatedAt] = GETDATE()
WHERE [Id] = @Id";
            return await UpdateAsync(query, actor);
        }

        public async Task<List<int>> GetActorsIdsByMovieIdAsync(int movieId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"
SELECT ActorId 
FROM Foundation.Actor_Movies 
WHERE MovieId = @movieId";

                    var parameters = new { movieId };

                    var actorIds = await conn.QueryAsync<int>(query, parameters);

                    return actorIds.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
