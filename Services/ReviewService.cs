using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieMania.Authentication;
using MovieMania.CustomExceptions;
using MovieMania.Models.Database;
using MovieMania.Models.Request;
using MovieMania.Models.Response;
using MovieMania.Repositories.Interfaces;
using MovieMania.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMania.Services
{
    public class ReviewService : IReviewService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMovieService _movieService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository reviewRepository,UserManager<ApplicationUser> userManager,IMapper mapper,IMovieService movieService)
        {
            _userManager = userManager;
            _movieService = movieService;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        public async Task ValidateReviewObjectAsync(ReviewRequest review)
        {
            if (string.IsNullOrWhiteSpace(review.Message) || string.IsNullOrWhiteSpace(review.UserName))
                throw new InvalidRequestObjectException("Review message and UserName is required");
            else if (review.Message.Length>1000)
                throw new InvalidRequestObjectException("Review message length should be than 1000 characters");
            var userExist = await _userManager.FindByNameAsync(review.UserName);
            if(userExist==null)
            {
                throw new InvalidRequestObjectException("UserName invalid");
            }
            try
            {
                var _ = await _movieService.GetAsync(review.MovieId);
            }
            catch(IdNotExistException ex) 
            { 
                throw new InvalidRequestObjectException(ex.Message);
            }
            
        }

        public async Task<int> CreateAsync(ReviewRequest review)
        {
            int id = 0;
            try
            {
                await ValidateReviewObjectAsync(review);

                id = await _reviewRepository.CreateAsync(_mapper.Map<Review>(review));
            }
            catch
            {
                throw;
            }

            return id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var _ = await GetByIdAsync(id);
                return await _reviewRepository.DeleteAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ReviewResponse>> GetAsync(int movieId)
        {
            
            List<Review> reviews = null;
            try
            {
                var _ = await _movieService.GetAsync(movieId);
                reviews = await _reviewRepository.GetAsync(movieId);
            }
            catch
            {
                throw;
            }

            if (reviews == null || reviews.Count == 0)
                return new List<ReviewResponse>();

            return reviews.Select(review => _mapper.Map<ReviewResponse>(review)).ToList();
        }

        public async Task<ReviewResponse> GetByIdAsync(int reviewId)
        {
            Review review;
            try
            {
                review = await _reviewRepository.GetByIdAsync(reviewId);
            }
            catch
            {
                throw;
            }

            if (review == null)
                throw new IdNotExistException($"The review with id : {reviewId} do not exist");
            return _mapper.Map<ReviewResponse>(review);
        }

        public async Task<int> UpdateAsync(ReviewRequest review, int id)
        {
            try
            {
                await ValidateReviewObjectAsync(review);
                var _ = await GetByIdAsync(id);
                Review updatedReview = _mapper.Map<Review>(review);
                updatedReview.Id = id;
                return await _reviewRepository.UpdateAsync(updatedReview);
            }
            catch
            {
                throw;
            }
        }
    }
}
