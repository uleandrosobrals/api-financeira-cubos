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

        public async Task<CardResponseDTO> CreateCardAsync(Guid accountId, CardCreateDTO cardCreateDTO)
        {
            
            var card = new Card
            {
                Type = cardCreateDTO.Type,
                Number = cardCreateDTO.Number,
                CVV = cardCreateDTO.CVV,
                AccountsId = accountId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            
            var createdCard = await _cardsRepository.AddCardAsync(accountId, card);

            
            var cardResponseDTO = new CardResponseDTO
            {
                Id = createdCard.Id,
                Type = createdCard.Type,
                Number = createdCard.Number,
                CVV = createdCard.CVV,
                CreatedAt = createdCard.CreatedAt,
                UpdatedAt = createdCard.UpdatedAt
            };

            return cardResponseDTO;
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


        public async Task<IEnumerable<CardResponseDTO>> GetCardsByPeopleAsync(Guid peopleId, int page, int itemsPerPage)
        {
            var accountIds = await _accountRepository.GetAccountsAsync(peopleId);

            var cardResponseDTOs = new List<CardResponseDTO>();
            foreach (var accountDto in accountIds)
            {
                var accountId = accountDto.Id;
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

                cardResponseDTOs.AddRange(cardDTOs);
            }


            var pagedCards = cardResponseDTOs
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage);

            return pagedCards;
        }


    }
}
