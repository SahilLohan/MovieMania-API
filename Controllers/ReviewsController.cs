using MovieMania.CustomExceptions;
using MovieMania.Models.Database;
using MovieMania.Models.Request;
using MovieMania.Services;
using MovieMania.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MovieMania.Authentication;

namespace MovieMania.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ReviewRequest review)
        {
            int id;
            try
            {
                id = await _reviewService.CreateAsync(review);
                return CreatedAtRoute(id, id);
            }
            catch (InvalidRequestObjectException ex)
            {
                return BadRequest(ex.Message);
            }  
        }
        [Authorize]
        [HttpGet("/movies/{movieId}/reviews")]
        public async Task<IActionResult> GetAsync([FromRoute]int movieId)
        {
            try
            {
                return Ok(await _reviewService.GetAsync(movieId));
            }
            catch(IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                return Ok(await _reviewService.GetByIdAsync(id));
            }
            catch (IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReviewRequest review)
        {
            try
            {
                int rows = await _reviewService.UpdateAsync(review, id);
                if (rows > 0)
                    return Ok("Review Updated");
                else
                    return BadRequest();
            }
            catch (IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidRequestObjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                int rows = await _reviewService.DeleteAsync(id);
                if (rows > 0)
                    return Ok("Review Deleted");
                else
                    return BadRequest();
            }
            catch (IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
