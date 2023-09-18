using DOMAIN.DTOs;
using DOMAIN.Entities;
using DOMAIN.Interfaces.Repositories;
using INFRA.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRA.Repositories
{
    public class AccountsRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Accounts> GetById(Guid id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Accounts> GetAccountByIdAsync(Guid accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<ICollection<Accounts>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Accounts> CreateAsync(Accounts account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<IEnumerable<AccountResponseDTO>> GetAccountsAsync(Guid peopleId)
        {
            var accounts = await _context.Accounts.Where(a => a.PeopleId == peopleId).ToListAsync();

            var responseDTOs = accounts.Select(account => new AccountResponseDTO
            {
                Id = account.Id,
                Branch = account.Branch,
                Account = account.Account,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt
            });

            return responseDTOs;
        }
    }

}
