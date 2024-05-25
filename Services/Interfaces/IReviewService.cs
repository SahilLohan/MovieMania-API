using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMania.Models.Request;
using MovieMania.Models.Response;
namespace MovieMania.Services.Interfaces
{
    public interface IReviewService
    {
        Task ValidateReviewObjectAsync(ReviewRequest review);
        Task<List<ReviewResponse>> GetAsync(int movieId);
        Task<ReviewResponse> GetByIdAsync(int id);
        Task<int> CreateAsync(ReviewRequest review);
        Task<int> UpdateAsync(ReviewRequest review, int id);
        Task<int> DeleteAsync(int id);
    }
}
