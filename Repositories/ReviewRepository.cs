using Dapper;
using MovieMania.Models.Database;
using MovieMania.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMania.Repositories
{
    public class ReviewRepository : BaseRepository<Review>,IReviewRepository
    {
        private readonly string _connectionString;
        public ReviewRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {
            _connectionString = connectionString.Value.IMDBDatabaseConnectionString;
        }
        public async Task<int> CreateAsync(Review review)
        {
            string query = @"
INSERT INTO Foundation.Reviews (
	[Message]
	,[MovieId]
	)
VALUES (
	@Message
	,@MovieId
	);";

            return await CreateAsync(query, review);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteStoredProcedureAsync("usp_DeleteReview", id);
        }

        public async Task<int> DeleteReviewOfMovieAsync(int movieId)
        {
            int count=0;
            List<Review> reviews = await GetAsync(movieId);
            foreach(var review in reviews)
            {
                count += await DeleteAsync(review.Id);
            }
            return count;
        }

        public async Task<List<Review>> GetAsync(int movieid)
        {
            string query = @"
SELECT [Id]
	,[Message]
	,[MovieId]
FROM Foundation.Reviews
WHERE MovieId = @movieId;";
            SqlConnection connection = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    connection = conn;
                    await conn.OpenAsync();
                    var reviews = await conn.QueryAsync<Review>(query, new {movieid});
                    await conn.CloseAsync(); // or conn.Dispose();
                    return reviews.ToList();
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

        public async Task<Review> GetByIdAsync(int id)
        {
            string query = @"
SELECT [Id]
	,[Message]
	,[MovieId]
FROM Foundation.Reviews
WHERE [Id] = @id;";
            SqlConnection connection = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    connection = conn;
                    await conn.OpenAsync();
                    var review = await conn.QueryFirstOrDefaultAsync<Review>(query, new { id });
                    await conn.CloseAsync(); // or conn.Dispose();
                    return review;
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

        public async Task<int> UpdateAsync(Review review)
        {
            string query = @"
UPDATE Foundation.Reviews
SET [Message] = @Message
	,[MovieId] = @MovieId
	,[UpdatedAt] = GETDATE()
WHERE Id = @Id;";
            return await UpdateAsync(query, review);
        }
    }
}
