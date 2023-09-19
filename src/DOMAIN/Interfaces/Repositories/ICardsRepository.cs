using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Repositories
{
    public interface ICardsRepository
    {
        Task<Card> AddCardAsync(Guid accountId, Card card);
        Task<Accounts> GetAccountAsync(Guid accountId);
        Task<List<Card>> GetCardsAsync(Guid accountId);
        Task<int> GetTotalCardsByPeopleAsync(Guid peopleId);
    }
}
