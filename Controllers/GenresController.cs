using MovieMania.CustomExceptions;
using MovieMania.Models.Database;
using MovieMania.Models.Request;
using MovieMania.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace MovieMania.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GenreRequest genre)
        {
            try
            {
                int id = await _genreService.CreateAsync(genre);
                return CreatedAtRoute(id, id);
            }
            catch (InvalidRequestObjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            return Ok(await _genreService.GetAsync());
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var result = await _genreService.GetAsync(id);
                return Ok(result);
            }
            catch (IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] GenreRequest genre)
        {
            try
            {
                int rows = await _genreService.UpdateAsync(genre, id);
                if (rows > 0)
                    return Ok("Genre Updated");
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
                int rows = await _genreService.DeleteAsync(id);
                if (rows > 0)
                    return Ok("Genre Deleted");
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
