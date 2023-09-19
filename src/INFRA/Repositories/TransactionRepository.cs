using DOMAIN.DTOs;
using DOMAIN.Entities;
using DOMAIN.Exceptions;
using DOMAIN.Interfaces.Repositories;
using INFRA.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async Task<Transaction> CreateTransactionAsync(Guid accountId, Transaction transaction)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }

            if (transaction.Value == 0)
            {
                throw new BusinessException("O valor da transação deve ser diferente de zero.");
            }

            var newTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Value = transaction.Value,
                Description = transaction.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountsId = accountId
            };

            account.Balance += transaction.Value;

            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync();

            return newTransaction;
        }

        public async Task<Transaction> GetTransactionAsync(Guid accountId, Guid transactionId)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }

            var transaction = await _context.Transactions.SingleOrDefaultAsync(t => t.AccountsId == accountId && t.Id == transactionId);
            if (transaction == null)
            {
                throw new BusinessException("Transação não encontrada.");
            }

            return transaction;
        }


        public async Task<List<Transaction>> GetTransactionsAsync(Guid accountId, int page, int itemsPerPage)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountsId == accountId)
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            return transactions;
        }

        public async Task<decimal> GetAccountBalanceAsync(Guid accountId)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }

            return account.Balance;
        }


        public async Task<Transaction> RevertTransactionAsync(Guid accountId, Guid transactionId)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }

            var originalTransaction = await _context.Transactions.SingleOrDefaultAsync(t => t.AccountsId == accountId && t.Id == transactionId);
            if (originalTransaction == null)
            {
                throw new BusinessException("Transação não encontrada.");
            }


            var reversalTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Value = -originalTransaction.Value,
                Description = "Reversão da transação " + originalTransaction.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountsId = accountId
            };

            account.Balance += reversalTransaction.Value;

            _context.Transactions.Add(reversalTransaction);
            await _context.SaveChangesAsync();

            return reversalTransaction;
        }
    }
}

