using MovieMania.CustomExceptions;
using MovieMania.Models.Request;
using MovieMania.Services;
using MovieMania.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MovieMania.Authentication;

namespace MovieMania.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProducerRequest producer)
        {
            try
            {
                int id = await _producerService.CreateAsync(producer);
                return CreatedAtRoute(id, id);
            }
            catch (InvalidRequestObjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _producerService.GetAsync());
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var result = await _producerService.GetAsync(id);
                return Ok(result);
            }
            catch (IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ProducerRequest producer)
        {
            try
            {
                int rows = await _producerService.UpdateAsync(producer, id);
                if (rows > 0)
                    return Ok("Producer Updated");
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
                int rows = await _producerService.DeleteAsync(id);
                if (rows > 0)
                    return Ok("Producer Deleted");
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
