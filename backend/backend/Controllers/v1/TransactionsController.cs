using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using backend.Dtos;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        private int CurrentUserId => int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll(CancellationToken cancellationToken)
        {
            Result<IEnumerable<TransactionDto>> result = await _transactionService.GetAllAsync(CurrentUserId, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TransactionDto>> GetById(int id, CancellationToken cancellationToken)
        {
            Result<TransactionDto> result = await _transactionService.GetByIdAsync(CurrentUserId, id, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> Create([FromBody] CreateTransactionDto transactionDto)
        {
            Result<TransactionDto> result = await _transactionService.CreateAsync(CurrentUserId, transactionDto);
            if (!result.IsSuccess) return this.ToActionResult(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TransactionDto>> Update(int id,  [FromBody] UpdateTransactionDto transactionDto)
        {
            Result<TransactionDto> result = await _transactionService.UpdateAsync(CurrentUserId, id, transactionDto);
            return this.ToActionResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await _transactionService.DeleteAsync(CurrentUserId, id);
            return this.ToActionResult(result);
        }
    }
}
