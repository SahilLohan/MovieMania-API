using AutoMapper;
using MovieMania.CustomExceptions;
using MovieMania.Helpers.Filters;
using MovieMania.Models.Database;
using MovieMania.Models.Request;
using MovieMania.Models.Response;
using MovieMania.Repositories;
using MovieMania.Repositories.Interfaces;
using MovieMania.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MovieMania.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorService _actorService;
        private readonly IGenreService _genreService;
        private readonly IProducerService _producerService;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IActorService actorService, IGenreService genreService, IProducerService producerService, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _actorService = actorService;
            _genreService = genreService;
            _producerService = producerService;
            _mapper = mapper;
        }

        public async Task ValidateMovieObjectAsync(MovieRequest movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Name))
                throw new InvalidRequestObjectException("Movie Name is required");
            else if (movie.YearOfRelease < 1888)
                throw new InvalidRequestObjectException("First movie of world was released in 1888 :). So, Fill correct year of release");
            else if (movie.Plot.Length > 1000)
                throw new InvalidRequestObjectException("Plot length should be less than 1000");

            try
            {
                var obj = await _producerService.GetAsync(movie.ProducerId);
            }
            catch(IdNotExistException ex)
            {
                throw new InvalidRequestObjectException(ex.Message);
            }

            if (movie.Actors==null || movie.Actors.Count==0)
                throw new InvalidRequestObjectException("There should be atleast 1 actor");
            if(movie.Genres == null || movie.Genres.Count == 0)
                throw new InvalidRequestObjectException("There should be atleast 1 genre");

            foreach (var genId in movie.Genres)
            {
                
                try
                {
                    var genre = await _genreService.GetAsync(genId);
                }
                catch (IdNotExistException ex)
                {
                    throw new InvalidRequestObjectException(ex.Message);
                }
            }
            foreach (var actId in movie.Actors)
            {
                
                try
                {
                    var actor = await _actorService.GetAsync(actId);
                }
                catch (IdNotExistException ex)
                {
                    throw new InvalidRequestObjectException(ex.Message);
                }
            }
        }

        public async Task<int> CreateAsync(MovieRequest movie)
        { 
            
            int id;
            try
            {
                await ValidateMovieObjectAsync(movie);
                id = await _movieRepository.CreateAsync(_mapper.Map<Movie>(movie), movie.Actors, movie.Genres);
            }
            catch
            {
                throw;
            }
            return id;
        }

        public async Task<List<MovieResponse>> GetAsync(MovieFilter parameters)
        {
            List<MovieResponse> result = new List<MovieResponse>();
            try
            {
                List<Movie> movies = await _movieRepository.GetAsync();

                if(parameters.Year!=0)
                    movies = movies.Where(m => m.YearOfRelease == parameters.Year).ToList();
                if(!string.IsNullOrWhiteSpace(parameters.Search))
                    movies = movies.Where(m=>m.Name.ToLower().Contains(parameters.Search.ToLower())).ToList();
                
                foreach (var movie in movies)
                {
                    MovieResponse movieUpdated = await GetAsync(movie.Id);
                    result.Add(movieUpdated);
                }
            }
            catch
            {
                throw;
            }
            if (result == null && result.Count == 0)
                return new List<MovieResponse>();

            return result;
        }

        public async Task<MovieResponse> GetAsync(int id)
        {
            MovieResponse result = null;
            try
            {
                Movie movie = await _movieRepository.GetAsync(id);
                if(movie == null)
                {
                    throw new IdNotExistException($"The movie with id : {id} do not exist");
                }
                result = _mapper.Map<MovieResponse>(movie);
                result.Actors = await _actorService.GetActorsByMovieIdAsync(id);
                result.Genres = await _genreService.GetGenresByMovieIdAsync(id);
                result.Producer = await _producerService.GetAsync(movie.ProducerId);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<int> UpdateAsync(MovieRequest movie, int id)
        {
            try
            {
                var _ = await GetAsync(id);
                await ValidateMovieObjectAsync(movie);
                Movie updatedMovie = _mapper.Map<Movie>(movie);
                updatedMovie.Id = id;
                return await _movieRepository.UpdateAsync(updatedMovie, movie.Actors, movie.Genres);
            }
            catch
            {
                throw;
            }        
        }
        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var _ = await GetAsync(id);
                return await _movieRepository.DeleteAsync(id);
            }
            catch
            {
                throw;
            }           
        }
    }
}
