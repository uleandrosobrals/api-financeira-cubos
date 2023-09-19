using DOMAIN.DTOs;
using DOMAIN.Exceptions;
using DOMAIN.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("people/{peopleId}/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<AccountResponseDTO>> CreateAccount(Guid peopleId, [FromBody] AccountCreateDTO createDTO)
        {
            try
            {
                var result = await _accountService.CreateAccountAsync(peopleId, createDTO);
                return CreatedAtAction(nameof(GetAccount), new { peopleId = peopleId, id = result.Id }, result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountResponseDTO>>> GetAccountsAsync(Guid peopleId)
        {
            var result = await _accountService.GetAccountsAsync(peopleId);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAccount")]
        public async Task<ActionResult<AccountResponseDTO>> GetAccount(Guid peopleId, Guid id)
        {
            var account = await _accountService.GetAccountByIdAsync(peopleId, id);

            if (account == null)
            {
                return NotFound("Conta não encontrada.");
            }

            return Ok(account);
        }


    }
}
