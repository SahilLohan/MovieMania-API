using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMania.Models.Request;
using MovieMania.Models.Response;
namespace MovieMania.Services.Interfaces
{
    public interface IProducerService
    {
        void ValidateProducerObject(ProducerRequest producer);
        Task<List<ProducerResponse>> GetAsync();
        Task<ProducerResponse> GetAsync(int id);
        Task<int> CreateAsync(ProducerRequest producer);
        Task<int> UpdateAsync(ProducerRequest producer, int id);
        Task<int> DeleteAsync(int id);
    }
}
