using DOMAIN.DTOs;
using DOMAIN.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/accounts/{accountId}/cards")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<ActionResult<CardResponseDTO>> CreateCard(Guid accountId, [FromBody] CardCreateDTO cardCreateDTO)
        {
            try
            {
                var createdCard = await _cardService.CreateCardAsync(accountId, cardCreateDTO);
                return Ok(createdCard);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<CardListResponseDTO>> GetCards(Guid accountId)
        {
            try
            {
                var cards = (await _cardService.GetCardsAsync(accountId)).ToList();
                return Ok(new CardListResponseDTO { Cards = cards });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("people/{personId}/cards")]
        public async Task<ActionResult<PagedCardListResponseDTO>> GetCardsByPerson(Guid personId, [FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10)
        {
            try
            {
                var cards = await _cardService.GetCardsByPeAsync(personId, page, itemsPerPage);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }






    }
}

