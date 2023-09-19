using DOMAIN.DTOs;
using DOMAIN.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpPost]
        public async Task<ActionResult<PeopleResponseDTO>> CreatePeople([FromBody] PeopleCreateDTO peopleCreateDTO)
        {
            var result = await _peopleService.CreatePeopleAsync(peopleCreateDTO);

            if (result == null)
            {
                return BadRequest("Falha ao criar a pessoa.");
            }

            return Ok(result);
        }
    }
}





