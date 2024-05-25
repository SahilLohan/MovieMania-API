using MovieMania.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MovieMania.Repositories.Interfaces
{
    public interface IProducerRepository
    {
        Task<List<Producer>> GetAsync();
        Task<Producer> GetAsync(int id);
        Task<int> CreateAsync(Producer producer);
        Task<int> UpdateAsync(Producer producer);
        Task<int> DeleteAsync(int id);
    }
}
