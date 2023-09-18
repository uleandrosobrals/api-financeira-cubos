using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransactionAsync(Guid accountId, Transaction transaction);
        Task<Transaction> GetTransactionAsync(Guid accountId, Guid transactionId);
        Task<List<Transaction>> GetTransactionsAsync(Guid accountId, int page, int itemsPerPage);
        Task<decimal> GetAccountBalanceAsync(Guid accountId);
        Task<Transaction> RevertTransactionAsync(Guid accountId, Guid transactionId);
    }
}
