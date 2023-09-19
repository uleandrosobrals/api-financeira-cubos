using DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Services
{
    public interface ICardService
    {
        Task<CardResponseDTO> CreateCardAsync(Guid accountId, CardCreateDTO createDTO);
        Task<IEnumerable<CardResponseDTO>> GetCardsAsync(Guid accountId);
        Task<IEnumerable<CardResponseDTO>> GetCardsByPeopleAsync(Guid peopleId, int page, int itemsPerPage);
       
    }
}
