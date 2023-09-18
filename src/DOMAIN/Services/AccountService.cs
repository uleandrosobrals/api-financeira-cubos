using DOMAIN.DTOs;
using DOMAIN.Entities;
using DOMAIN.Exceptions;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountService _accountService;
        private readonly IPeopleService _peopleService;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountService accountService, IPeopleService peopleService, IAccountRepository accountRepository)
        {
            _accountService = accountService;
            _peopleService = peopleService;
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponseDTO> CreateAccountAsync(Guid PeopleId, AccountCreateDTO createDTO)
        {
            var existingPeople = await _peopleService.GetPeopleAsync(PeopleId);
            if (existingPeople == null)
            {
                throw new BusinessException("Pessoa não encontrada");
            }

            var newAccount = new Accounts
            {
                Branch = createDTO.Branch,
                Account = createDTO.Account,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Balance = 0, 
                PeopleId = PeopleId 
            };

            var createdAccount = await _accountRepository.CreateAsync(newAccount);

            var responseDTO = new AccountResponseDTO
            {
                Id = createdAccount.Id,
                Branch = createdAccount.Branch,
                Account = createdAccount.Account,
                CreatedAt = createdAccount.CreatedAt,
                UpdatedAt = createdAccount.UpdatedAt
            };

            return responseDTO;
        }

        public async Task<AccountResponseDTO> GetAccountByIdAsync(Guid PeopleId, Guid accountId)
        {

            var existingPeople = await _peopleService.GetPeopleAsync(PeopleId);
            if (existingPeople == null)
            {
                throw new BusinessException("Pessoa não encontrada");
            }

            var account = await _accountRepository.GetAccountByIdAsync(accountId);

            if (account == null || account.PeopleId != PeopleId)
            {
                return null; 
            }

            var accountDTO = new AccountResponseDTO
            {
                Id = account.Id,
                Branch = account.Branch,
                Account = account.Account,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt
            };

            return accountDTO;


        }

        public async Task<IEnumerable<AccountResponseDTO>> GetAccountsAsync(Guid peopleId)
        {
            var existingPeople = await _peopleService.GetPeopleAsync(peopleId);
            if (existingPeople == null)
            {
                throw new BusinessException("Pessoa não encontrada");
            }

            var accounts = await _accountRepository.GetAccountsAsync(peopleId);

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
