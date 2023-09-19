using DOMAIN.DTOs;
using System;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponseDTO> CreateTransactionAsync(Guid accountId, TransactionCreateDTO createDTO);
        Task<TransactionResponseDTO> GetTransactionAsync(Guid accountId, Guid transactionId);
        Task<TransactionListResponseDTO> GetTransactionsAsync(Guid accountId, int page, int itemsPerPage);
        Task<BalanceResponseDTO> GetAccountBalanceAsync(Guid accountId);
        Task<TransactionRevertDTO> RevertTransactionAsync(Guid accountId, Guid transactionId);
    }
}
