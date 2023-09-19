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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Transaction> CreateTransactionAsync(Guid accountId, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetAccountBalanceAsync(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetTransactionAsync(Guid accountId, Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaction>> GetTransactionsAsync(Guid accountId, int page, int itemsPerPage)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> RevertTransactionAsync(Guid accountId, Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
