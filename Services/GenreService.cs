using AutoMapper;
using MovieMania.CustomExceptions;
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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        public void ValidateGenreObject(GenreRequest genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name))
                throw new InvalidRequestObjectException("Genre name is required");
        }

        public async Task<int> CreateAsync(GenreRequest genre)
        {
            int id;
            try
            {
                ValidateGenreObject(genre);
                id = await _genreRepository.CreateAsync(_mapper.Map<Genre>(genre));
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
                return await _genreRepository.DeleteAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<GenreResponse>> GetAsync()
        {
            List<Genre> genres = null;
            try
            {
                genres = await _genreRepository.GetAsync();
            }
            catch
            {
                throw;
            }

            if (genres == null || genres.Count == 0)
                return new List<GenreResponse>();

            return genres.Select(genre => _mapper.Map<GenreResponse>(genre)).ToList();
        }

        public async Task<GenreResponse> GetAsync(int id)
        {
            Genre genre;
            try
            {
                genre = await _genreRepository.GetAsync(id);
            }
            catch
            {
                throw;
            }

            if (genre == null)
                throw new IdNotExistException($"The genre with id : {id} do not exist");
            return _mapper.Map<GenreResponse>(genre);
        }

        public async Task<int> UpdateAsync(GenreRequest genre, int id)
        {
            try
            {
                var _ = await GetAsync(id);
                ValidateGenreObject(genre);
                Genre updatedGenre = _mapper.Map<Genre>(genre);
                updatedGenre.Id = id;
                return await _genreRepository.UpdateAsync(updatedGenre);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<GenreResponse>> GetGenresByMovieIdAsync(int movieId)
        {
            List<int> genresIds = await _genreRepository.GetGenresIdsByMovieIdAsync(movieId);

            List<GenreResponse> result = new List<GenreResponse>();
            GenreResponse genre = null;
            foreach (int genreId in genresIds)
            {
                genre = await GetAsync(genreId);
                result.Add(genre);
            }
            return result;
        }
    }
}
