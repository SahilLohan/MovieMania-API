using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMania.Models.Database;
namespace MovieMania.Repositories.Interfaces
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetAsync();
        Task<Actor> GetAsync(int id);
        Task<int> CreateAsync(Actor actor);
        Task<int> UpdateAsync(Actor actor);
        Task<int> DeleteAsync(int id);
        public Task<List<int>> GetActorsIdsByMovieIdAsync(int movieId);

    }
}
