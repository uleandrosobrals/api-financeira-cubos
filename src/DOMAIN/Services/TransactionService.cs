using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOMAIN.DTOs;
using DOMAIN.Entities;
using DOMAIN.Exceptions;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Interfaces.Services;

namespace DOMAIN.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<TransactionResponseDTO> CreateTransactionAsync(Guid accountId, TransactionCreateDTO createDTO)
        {
            var account = await _accountRepository.GetAccountAsync(accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Value = createDTO.Value,
                Description = createDTO.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountsId = accountId
            };

            if (createDTO.Value > 0)
            {
                account.Balance += createDTO.Value;
            }
            else
            {
                var debitAmount = Math.Abs(createDTO.Value);
                if (account.Balance < debitAmount)
                {
                    throw new BusinessException("Saldo insuficiente para realizar a transação.");
                }

                account.Balance -= debitAmount;
            }

            await _transactionRepository.CreateTransactionAsync(accountId, transaction);

            return new TransactionResponseDTO
            {
                Id = transaction.Id,
                Value = transaction.Value,
                Description = transaction.Description,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt
            };
        }

        public async Task<TransactionResponseDTO> GetTransactionAsync(Guid accountId, Guid transactionId)
        {

            var account = await _accountRepository.GetAccountAsync(accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }

            var transaction = await _transactionRepository.GetTransactionAsync(accountId, transactionId);
            if (transaction == null)
            {
                throw new BusinessException("Transação não encontrada.");
            }

            return new TransactionResponseDTO
            {
                Id = transaction.Id,
                Value = transaction.Value,
                Description = transaction.Description,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt
            };
        }

        public async Task<TransactionListResponseDTO> GetTransactionsAsync(Guid accountId, int page, int itemsPerPage)
        {

            var account = await _accountRepository.GetAccountAsync(accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }

            var transactions = await _transactionRepository.GetTransactionsAsync(accountId, page, itemsPerPage);

            var transactionDTOs = transactions.Select(transaction => new TransactionResponseDTO
            {
                Id = transaction.Id,
                Value = transaction.Value,
                Description = transaction.Description,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt
            }).ToList();

            return new TransactionListResponseDTO
            {
                Transactions = transactionDTOs
            };
        }

        public async Task<BalanceResponseDTO> GetAccountBalanceAsync(Guid accountId)
        {

            var account = await _accountRepository.GetAccountAsync(accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }

            return new BalanceResponseDTO
            {
                Balance = account.Balance
            };
        }

        public async Task<TransactionRevertDTO> RevertTransactionAsync(Guid accountId, Guid transactionId)
        {

            var account = await _accountRepository.GetAccountAsync(accountId);
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.");
            }


            var transaction = await _transactionRepository.GetTransactionAsync(accountId, transactionId);
            if (transaction == null)
            {
                throw new BusinessException("Transação não encontrada.");
            }

            decimal reverseValue = transaction.Value > 0 ? -transaction.Value : Math.Abs(transaction.Value);

            var reversedTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Value = reverseValue,
                Description = "Estorno de transação",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountsId = accountId
            };

            await _transactionRepository.CreateTransactionAsync(accountId, reversedTransaction);

            account.Balance += reverseValue;

            return new TransactionRevertDTO
            {
                Id = reversedTransaction.Id,
                Value = reversedTransaction.Value,
                Description = reversedTransaction.Description,
                CreatedAt = reversedTransaction.CreatedAt,
                UpdatedAt = reversedTransaction.UpdatedAt
            };
        }
    }
}
