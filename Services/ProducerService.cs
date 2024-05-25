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
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IMapper _mapper;

        public ProducerService(IProducerRepository producerRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _mapper = mapper;
        }

        public void ValidateProducerObject(ProducerRequest producer)
        {
            if (string.IsNullOrWhiteSpace(producer.Name))
                throw new InvalidRequestObjectException("Producer name is required");
            else if (string.IsNullOrWhiteSpace(producer.Gender))
                throw new InvalidRequestObjectException("Producer gender is required");
            else if (producer.Gender != "female" && producer.Gender != "male" && producer.Gender != "non-binary")
                throw new InvalidRequestObjectException("Gender can only be - male , female , non-binary");
            else if (producer.Bio.Length > 500)
                throw new InvalidRequestObjectException("producer Bio should be less than 500 characters");
            else if (producer.DOB.Year < 1800)
                throw new InvalidRequestObjectException("Producer DOB can not be before 1800");
        }

        public async Task<int> CreateAsync(ProducerRequest producer)
        {            
            int id;
            try
            {
                ValidateProducerObject(producer);
                id = await _producerRepository.CreateAsync(_mapper.Map<Producer>(producer));
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
                var _ =  await GetAsync(id);
                return await _producerRepository.DeleteAsync(id);
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<ProducerResponse>> GetAsync()
        {
            List<Producer> producers = null;
            try
            {
                producers = await _producerRepository.GetAsync();
            }
            catch
            {
                throw;
            }

            if (producers == null || producers.Count == 0)
                return new List<ProducerResponse>();

            return producers.Select(producer => _mapper.Map<ProducerResponse>(producer)).ToList();
        }

        public async Task<ProducerResponse> GetAsync(int id)
        {
            Producer producer;
            try
            {
                producer = await _producerRepository.GetAsync(id);
            }
            catch
            {
                throw;
            }

            if (producer == null)
                throw new IdNotExistException($"The producer with id : {id} do not exist");
            return _mapper.Map<ProducerResponse>(producer);
        }

        public async Task<int> UpdateAsync(ProducerRequest producer, int id)
        {
            try
            {
                var _ = await GetAsync(id);
                ValidateProducerObject(producer);

                Producer updatedProducer = _mapper.Map<Producer>(producer);
                updatedProducer.Id = id;
                return await _producerRepository.UpdateAsync(updatedProducer);
            }
            catch
            {
                throw;
            }
        }
    }
}
