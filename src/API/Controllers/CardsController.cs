using DOMAIN.DTOs;
using DOMAIN.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }


        
        [HttpPost("accounts/{accountId}/cards")]
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

        [HttpGet("accounts/{accountId}/cards")]
        public async Task<ActionResult<IEnumerable<CardResponseDTO>>> GetCardsByAccount(Guid accountId)
        {
            try
            {
                var accountCards = await _cardService.GetCardsAsync(accountId);
                return Ok(accountCards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("people/{peopleId}/cards")]
        public async Task<ActionResult<IEnumerable<CardResponseDTO>>> GetCardsByPeople(Guid peopleId, [FromQuery] int page = 1, [FromQuery] int itemsPerPage = 5)
        {
            try
            {
                var cards = await _cardService.GetCardsByPeopleAsync(peopleId, page, itemsPerPage);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
