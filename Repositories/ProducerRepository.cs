using MovieMania.Models.Database;
using MovieMania.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieMania.Repositories
{
    public class ProducerRepository : BaseRepository<Producer>,IProducerRepository
    {

        private readonly string _connectionString;
        public ProducerRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {
            _connectionString = connectionString.Value.IMDBDatabaseConnectionString;
        }
        public async Task<int> CreateAsync(Producer producer)
        {
            string query = @"
INSERT INTO Foundation.Producers (
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

            return await CreateAsync(query, producer);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await ExecuteStoredProcedureAsync("usp_DeleteProducer", new { Id = id });
        }

        public async Task<List<Producer>> GetAsync()
        {
            string query = @"
SELECT [Id]
	,[Name]
	,[Gender]
	,[DOB]
	,[Bio]
FROM Foundation.Producers;";
            return await GetAsync(query);
        }

        public async Task<Producer> GetAsync(int id)
        {
            string query = @"
SELECT [Id]
	,[Name]
	,[Gender]
	,[DOB]
	,[Bio]
FROM Foundation.Producers
WHERE Id = @id;";
            return await GetAsync(query, new { id });
        }

        public async Task<int> UpdateAsync(Producer producer)
        {
            string query = @"
UPDATE Foundation.Producers
SET [Name] = @Name
	,[Gender] = @Gender
	,[DOB] = @DOB
	,[Bio] = @Bio
	,[UpdatedAt] = GETDATE()
WHERE [Id] = @Id;";
            return await UpdateAsync(query, producer);
        }
    }
}