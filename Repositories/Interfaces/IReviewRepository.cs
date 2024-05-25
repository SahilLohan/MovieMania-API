using MovieMania.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MovieMania.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAsync(int movieId);
        Task<Review> GetByIdAsync(int id);
        Task<int> CreateAsync(Review review);
        Task<int> UpdateAsync(Review review);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteReviewOfMovieAsync(int id);
    }
}
