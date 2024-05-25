using MovieMania.CustomExceptions;
using MovieMania.Models.Request;
using MovieMania.Models.Response;
using MovieMania.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Storage;
using Microsoft.Data.SqlClient;
using MovieMania.Helpers.Filters;
using Microsoft.Extensions.Options;
/*
 * Controller should have proper methods to handle following operation with usage of HTTP Verbs
    - Create - POST /resources
    - Get all - GET /resources
    - Get by Id - GET /resources/{resourceId}
    - Update - PUT /resources/{resourceId}
    - Delete - DELETE /resources/{resourceId}
*/
namespace MovieMania.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly string _firebaseConnectionString;

        public MoviesController(IMovieService movieService, IOptions<ConnectionString> connectionString)
        {
            _movieService = movieService;
            _firebaseConnectionString = connectionString.Value.FirebaseConnectionString;

        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MovieRequest movie)
        {
            try
            {
                int id = await _movieService.CreateAsync(movie);
                return CreatedAtRoute(id, id);
            }
            catch (InvalidRequestObjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] MovieFilter parameters)
        {
            List<MovieResponse> result = new();
            result = await _movieService.GetAsync(parameters);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var result = await _movieService.GetAsync(id);
                return Ok(result);
            }
            catch (IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] MovieRequest movie)
        {
            try
            {
                int rows = await _movieService.UpdateAsync(movie, id);
                if (rows > 0)
                    return Ok("Movie Updated");
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                int rows = await _movieService.DeleteAsync(id);
                if (rows > 0)
                    return Ok("Movie Deleted");
                else
                    return BadRequest();
            }
            catch (IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");
            try
            {
                var task = await new FirebaseStorage(_firebaseConnectionString)
                    .Child("MovieImages")
                    .Child(Guid.NewGuid().ToString() + ".jpg")
                    .PutAsync(file.OpenReadStream());
                return Ok(task);
            }
            catch
            {
                return StatusCode(500);
            } 
        }

    }
}
