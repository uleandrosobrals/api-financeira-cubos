using DOMAIN.DTOs;
using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Accounts> GetById(Guid id);
        Task<Accounts> GetAccountByIdAsync(Guid accountId);
        Task<ICollection<Accounts>> GetAll();
        Task<Accounts> CreateAsync(Accounts account);
        Task<IEnumerable<AccountResponseDTO>> GetAccountsAsync(Guid peopleId);
        Task<Accounts> GetAccountAsync(Guid accountId);
    }

}
