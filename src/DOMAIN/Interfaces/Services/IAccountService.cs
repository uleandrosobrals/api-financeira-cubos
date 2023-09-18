using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.DTOs;

namespace DOMAIN.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AccountResponseDTO> CreateAccountAsync(Guid PeopleId, AccountCreateDTO createDTO);
        Task<IEnumerable<AccountResponseDTO>> GetAccountsAsync(Guid PeopleId);
        Task<AccountResponseDTO> GetAccountByIdAsync(Guid PeopleId, Guid accountId);
        Task<decimal> GetAccountBalanceAsync(Guid accountId);
    }
}
