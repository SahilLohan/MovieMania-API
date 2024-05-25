using AutoMapper;
using MovieMania.Models.Database;
using MovieMania.Models.Request;
using MovieMania.Models.Response;
using MovieMania.Repositories.Interfaces;
using MovieMania.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMania.CustomExceptions;

namespace MovieMania.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;
        public ActorService(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }
        public void ValidateActorObject(ActorRequest actor)
        {
            if (string.IsNullOrWhiteSpace(actor.Name))
                throw new InvalidRequestObjectException("Actor name is required");
            else if (string.IsNullOrWhiteSpace(actor.Gender))
                throw new InvalidRequestObjectException("Actor gender is required");
            else if (actor.Gender != "female" && actor.Gender != "male" && actor.Gender != "non-binary")
                throw new InvalidRequestObjectException("Gender can only be - male , female , non-binary");
            else if(actor.Bio.Length > 500)
                throw new InvalidRequestObjectException("Actor Bio should be less than 500 characters");
            else if(actor.DOB.Year < 1800)
                throw new InvalidRequestObjectException("Actor DOB can not be before 1800");
        }
        public async Task<int> CreateAsync(ActorRequest actor)
        {
            
            int id;
            try
            {
                ValidateActorObject(actor);
                id = await _actorRepository.CreateAsync(_mapper.Map<Actor>(actor));
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
                var _ = await GetAsync(id);
                return await _actorRepository.DeleteAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ActorResponse>> GetAsync()
        {
            List<Actor> actors = null;
            try
            {
                actors = await _actorRepository.GetAsync();
            }
            catch
            {
                throw;
            }
            
            if (actors == null || actors.Count == 0)
                return new List<ActorResponse>();

            return actors.Select(actor => _mapper.Map<ActorResponse>(actor)).ToList();
        }

        public async Task<ActorResponse> GetAsync(int id)
        {
            Actor actor;
            try
            {
                actor = await _actorRepository.GetAsync(id);
            }
            catch
            {
                throw;
            }
            
            if (actor == null)
                throw new IdNotExistException($"The actor with id : {id} do not exist");
            return _mapper.Map<ActorResponse>(actor);
        }

        public async Task<int> UpdateAsync(ActorRequest actor, int id)
        {
            
            try
            {
                ValidateActorObject(actor);
                var _ = await GetAsync(id);

                Actor updatedActor = _mapper.Map<Actor>(actor);
                updatedActor.Id = id;
                return await _actorRepository.UpdateAsync(updatedActor);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<ActorResponse>> GetActorsByMovieIdAsync(int movieId)
        {
            List<int> actors = await _actorRepository.GetActorsIdsByMovieIdAsync(movieId);
            List<ActorResponse> result = new List<ActorResponse>();
            ActorResponse actor = null;
            foreach (int actorId in actors)
            {
                actor = await GetAsync(actorId);
                result.Add(actor);
            }
            return result;
        }
    }
}
