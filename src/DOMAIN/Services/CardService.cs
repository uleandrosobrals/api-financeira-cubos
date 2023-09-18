using DOMAIN.DTOs;
using DOMAIN.Entities;
using DOMAIN.Exceptions;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOMAIN.Services
{
    public class CardService : ICardService
    {
        private readonly ICardsRepository _cardsRepository;
        private readonly IAccountRepository _accountRepository;

        public CardService(ICardsRepository cardsRepository, IAccountRepository accountRepository)
        {
            _cardsRepository = cardsRepository;
            _accountRepository = accountRepository;
        }

        public async Task<CardResponseDTO> CreateCardAsync(Guid accountId, CardCreateDTO createDTO)
        {
            var existingAccount = await _cardsRepository.GetAccountAsync(accountId);
            if (existingAccount == null)
            {
                throw new BusinessException("Conta não encontrada");
            }

            
            var newCard = new Card
            {
                Id = Guid.NewGuid(),
                Type = createDTO.Type,
                Number = createDTO.Number,
                CVV = createDTO.CVV,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccountsId = accountId 
            };

            await _cardsRepository.AddCardAsync(newCard);

            return new CardResponseDTO
            {
                Id = newCard.Id,
                Type = newCard.Type,
                Number = newCard.LastFourDigits,
                CVV = newCard.CVV,
                CreatedAt = newCard.CreatedAt,
                UpdatedAt = newCard.UpdatedAt
            };
        }

        public Task GetCardAsync(Guid accountId, Guid cardId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CardResponseDTO>> GetCardsAsync(Guid accountId)
        {
            var existingAccount = await _cardsRepository.GetAccountAsync(accountId);
            if (existingAccount == null)
            {
                throw new BusinessException("Conta não encontrada");
            }

            var cards = await _cardsRepository.GetCardsAsync(accountId);

            var cardDTOs = cards.Select(card => new CardResponseDTO
            {
                Id = card.Id,
                Type = card.Type,
                Number = card.LastFourDigits,
                CVV = card.CVV,
                CreatedAt = card.CreatedAt,
                UpdatedAt = card.UpdatedAt
            });

            return cardDTOs;
        }

        public async Task<IEnumerable<CardResponseDTO>> GetCardsByPeopleIdAsync(Guid peopleId)
        {
            var accountIds = await _accountRepository.GetAccountIdsByPeopleIdAsync(peopleId);

            var cardResponseDTOs = new List<CardResponseDTO>();
            foreach (var accountId in accountIds)
            {
                cardResponseDTOs.AddRange(await GetCardsAsync(accountId));
            }

            return cardResponseDTOs;
        }

        public async Task<PagedCardListResponseDTO> GetCardsByPersonAsync(Guid personId, int page, int itemsPerPage)
{
    var existingPerson = await _cardsRepository.GetPersonAsync(personId);
    if (existingPerson == null)
    {
        throw new BusinessException("Pessoa não encontrada");
    }

    var pagedResult = await _cardsRepository.GetCardsByPersonAsync(personId, page, itemsPerPage);

    var cardDTOs = pagedResult.Items.Select(card => new CardResponseDTO
    {
        Id = card.Id,
        Type = card.Type,
        Number = card.LastFourDigits,
        CVV = card.CVV,
        CreatedAt = card.CreatedAt,
        UpdatedAt = card.UpdatedAt
    });

    var pagedResponse = new PagedCardListResponseDTO
    {
        Cards = cardDTOs.ToList(),
        Pagination = new PaginationDTO
        {
            ItemsPerPage = itemsPerPage,
            CurrentPage = page,
            TotalItems = pagedResult.TotalItems
        }
    };

    return pagedResponse;
}



    }
}
