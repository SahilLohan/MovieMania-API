using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMania.Models.Request;
using MovieMania.Models.Response;
namespace MovieMania.Services.Interfaces
{
    public interface IGenreService
    {
        void ValidateGenreObject(GenreRequest genre);
        Task<List<GenreResponse>> GetAsync();
        Task<GenreResponse> GetAsync(int id);
        Task<int> CreateAsync(GenreRequest genre);
        Task<int> UpdateAsync(GenreRequest genre,int id);
        Task<int> DeleteAsync(int id);
        public Task<List<GenreResponse>> GetGenresByMovieIdAsync(int movieId);
    }
}
