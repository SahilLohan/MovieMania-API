using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMania.Helpers.Filters;
using MovieMania.Models.Request;
using MovieMania.Models.Response;
namespace MovieMania.Services.Interfaces
{
    public interface IMovieService
    {
        Task ValidateMovieObjectAsync(MovieRequest movie);
        Task<List<MovieResponse>> GetAsync(MovieFilter parameters);
        Task<MovieResponse> GetAsync(int id);
        Task<int> CreateAsync(MovieRequest movie);
        Task<int> UpdateAsync(MovieRequest movie,int id);
        Task<int> DeleteAsync(int id);
    }
}
