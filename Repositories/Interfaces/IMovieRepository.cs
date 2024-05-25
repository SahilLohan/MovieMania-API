using MovieMania.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MovieMania.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAsync();
        Task<Movie> GetAsync(int id);
        Task<int> CreateAsync(Movie movie, List<int> actorsIds, List<int> genresIds);
        Task<int> UpdateAsync(Movie movie, List<int> actorsIds, List<int> genresIds);
        Task<int> DeleteAsync(int id);
    }
}
