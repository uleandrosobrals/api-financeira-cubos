using DOMAIN.Entities;
using DOMAIN.Interfaces.Repositories;
using INFRA.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFRA.Repositories
{
    public class CardRepository : ICardsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CardRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCardAsync(Card card)
        {
            _dbContext.Cards.Add(card);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Accounts> GetAccountAsync(Guid accountId)
        {
            return await _dbContext.Accounts.FindAsync(accountId);
        }

        public async Task<List<Card>> GetCardsAsync(Guid accountId)
        {
            return await _dbContext.Cards
                .Where(card => card.AccountsId == accountId)
                .ToListAsync();
        }

        public Task<int> GetTotalCardsByPeopleAsync(Guid peopleId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> ListLast4DigitsAsync(Guid accountId)
        {
            return await _dbContext.Cards
                .Where(card => card.AccountsId == accountId)
                .Select(card => card.LastFourDigits)
                .ToListAsync();
        }

        public async Task<List<Card>> GetCardsByPeopleIdAsync(Guid peopleId)
        {
            return await _dbContext.Cards
                .Where(card => card.PeopleId == peopleId)
                .ToListAsync();
        }

        public async Task<PagedResult<Card>> GetCardsByPersonAsync(Guid personId, int page, int itemsPerPage)
        {
            var query = _dbContext.Cards
                .Where(card => card.PersonId == personId)
                .OrderBy(card => card.CreatedAt);

            var totalItems = await query.CountAsync();
            var cards = await query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();

            return new PagedResult<Card>
            {
                Items = cards,
                TotalItems = totalItems
            };
        }

        public async Task<int> GetTotalCardsByPersonAsync(Guid personId)
        {
            return await _dbContext.Cards
                .CountAsync(card => card.PersonId == personId);
        }

    }
}
