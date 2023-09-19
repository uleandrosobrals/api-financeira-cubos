using DOMAIN.Entities;
using DOMAIN.Interfaces.Repositories;
using INFRA.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOMAIN.Repositories
{
    public class CardsRepository : ICardsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CardsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Card> AddCardAsync(Guid accountId, Card card)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                throw new Exception("Conta não encontrada");
            }

            _dbContext.Cards.Add(card);
            await _dbContext.SaveChangesAsync();

            return card;
        }



        public async Task<Accounts> GetAccountAsync(Guid accountId)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task<List<Card>> GetCardsAsync(Guid accountId)
        {
            return await _dbContext.Cards.Where(card => card.AccountsId == accountId).ToListAsync();
        }



        public async Task<int> GetTotalCardsByPeopleAsync(Guid peopleId)
        {
            return await _dbContext.Cards
                      .Join(_dbContext.Accounts, card => card.AccountsId, account => account.Id, (card, account) => new { card, account })
                      .Where(joinResult => joinResult.account.PeopleId == peopleId)
                      .CountAsync();
        }
    }
}
