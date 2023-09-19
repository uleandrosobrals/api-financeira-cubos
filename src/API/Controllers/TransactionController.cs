using DOMAIN.DTOs;
using DOMAIN.Entities;
using DOMAIN.Exceptions;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/accounts/{accountId}/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public TransactionController(ITransactionService transactionService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<TransactionResponseDTO>> CreateTransaction(Guid accountId, [FromBody] TransactionCreateDTO transactionDTO)
        {
            try
            {
                var transaction = await _transactionService.CreateTransactionAsync(accountId, transactionDTO);

                return CreatedAtAction(nameof(GetTransaction), new { accountId, transactionId = transaction.Id }, transaction);
            }
            catch (BusinessException)
            {
                return NotFound("Conta não encontrada");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<TransactionResponseDTO>> GetTransaction(Guid accountId, Guid transactionId)
        {
            var transaction = await _transactionService.GetTransactionAsync(accountId, transactionId);
            if (transaction == null)
            {
                return NotFound("Transação não encontrada");
            }

            return Ok(transaction);
        }

        
        [HttpGet]
        public async Task<ActionResult<TransactionListResponseDTO>> GetTransaction(Guid accountId, [FromQuery] int page = 1, [FromQuery] int itemsPerPage = 5)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsAsync(accountId, page, itemsPerPage);
                return Ok(transactions);
            }
            catch (BusinessException)
            {
                return NotFound("Conta não encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }


        [HttpGet("balance")]
        public async Task<ActionResult<decimal>> GetAccountBalance(Guid accountId)
        {
            try
            {
                var balance = await _accountService.GetAccountBalanceAsync(accountId);
                return Ok(balance);
            }
            catch (BusinessException)
            {
                return NotFound("Conta não encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost("{transactionId}/revert")]
        public async Task<ActionResult<TransactionResponseDTO>> RevertTransaction(Guid accountId, Guid transactionId)
        {
            try
            {
                var revertedTransaction = await _transactionService.RevertTransactionAsync(accountId, transactionId);
                return Ok(revertedTransaction);
            }
            catch (BusinessException)
            {
                return NotFound("Transação não encontrada");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
