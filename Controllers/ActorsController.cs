using AutoMapper;
using MovieMania.CustomExceptions;
using MovieMania.Models.Request;
using MovieMania.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace MovieMania.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;

        public ActorsController(IActorService actorService,IMapper mapper)
        {
            _actorService = actorService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ActorRequest actor)
        {
            try
            {
                int id = await _actorService.CreateAsync(actor);
                return CreatedAtRoute(id, id);
            }
            catch (InvalidRequestObjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/giveAllActors")]
        //[Route("/ActorsDedeBhai")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _actorService.GetAsync());
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var result = await _actorService.GetAsync(id);
                return Ok(result);
            }
            catch (IdNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ActorRequest actor)
        {
            try
            {
                int rows = await _actorService.UpdateAsync(actor,id);
                if (rows > 0)
                    return Ok("Actor Updated");
                else
                    return BadRequest();
            }
            catch(IdNotExistException ex)
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
                int rows = await _actorService.DeleteAsync(id);
                if (rows > 0)
                    return Ok("Actor Deleted");
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
