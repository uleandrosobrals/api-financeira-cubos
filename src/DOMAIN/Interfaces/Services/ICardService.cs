using DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Services
{
    public interface ICardService
    {
        Task<CardResponseDTO> CreateCardAsync(Guid accountId, CardCreateDTO createDTO);
        Task GetCardAsync(Guid accountId, Guid cardId);
        Task<IEnumerable<CardResponseDTO>> GetCardsAsync(Guid accountId);
        Task GetCardsByPeopleAsync(Guid personId, int page, int itemsPerPage);
    }
}
