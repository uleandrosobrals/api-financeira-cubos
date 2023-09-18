using DOMAIN.DTOs;
using DOMAIN.Entities;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace DOMAIN.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task<PeopleResponseDTO> CreatePeopleAsync(PeopleCreateDTO createDTO)
        {

            var newPeople = new People
            {
                Id = Guid.NewGuid(),
                Name = createDTO.Name,
                Document = createDTO.Document,
                Password = createDTO.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdPeople = await _peopleRepository.CreateAsync(newPeople);

            return new PeopleResponseDTO
            {
                Id = createdPeople.Id,
                Name = createdPeople.Name,
                Document = createdPeople.Document,
                CreatedAt = createdPeople.CreatedAt,
                UpdatedAt = createdPeople.UpdatedAt
            };
        }

        public Task<IEnumerable<PeopleResponseDTO>> GetPeopleAsync(Guid peopleId)
        {
            throw new NotImplementedException();
        }

        
    }
}
