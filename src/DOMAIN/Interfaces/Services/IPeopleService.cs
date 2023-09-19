using DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Services
{
    public interface IPeopleService
    {
        Task<PeopleResponseDTO> CreatePeopleAsync(PeopleCreateDTO createDTO);
        Task<IEnumerable<PeopleResponseDTO>> GetPeopleAsync(Guid peopleId);

    }
}
