using MovieMania.Models.Request;
using MovieMania.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MovieMania.Services.Interfaces
{
    public interface IActorService
    {
        void ValidateActorObject(ActorRequest actor);
        Task<List<ActorResponse>> GetAsync();
        Task<ActorResponse> GetAsync(int id);
        Task<int> CreateAsync(ActorRequest actor);
        Task<int> UpdateAsync(ActorRequest actor,int id);
        Task<int> DeleteAsync(int id);
        public Task<List<ActorResponse>> GetActorsByMovieIdAsync(int movieId);
    }
}
