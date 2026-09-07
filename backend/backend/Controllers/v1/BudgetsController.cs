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
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        private int CurrentUserId => int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetStatusDto>>> GetAll(CancellationToken cancellationToken)
        {
            Result<IEnumerable<BudgetStatusDto>> result = await _budgetService.GetAllAsync(CurrentUserId, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BudgetStatusDto>> GetById(int id, CancellationToken cancellationToken)
        {
            Result<BudgetStatusDto> result = await _budgetService.GetByIdAsync(CurrentUserId, id, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<BudgetDto>> Create([FromBody] CreateBudgetDto newBudget)
        {
            Result<BudgetDto> result = await _budgetService.CreateAsync(CurrentUserId, newBudget);
            if(!result.IsSuccess) return this.ToActionResult(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BudgetDto>> Update(int id, [FromBody] UpdateBudgetDto newBudget) 
        {
            Result<BudgetDto> result = await _budgetService.UpdateAsync(CurrentUserId, id, newBudget);
            return this.ToActionResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await _budgetService.DeleteAsync(CurrentUserId, id);
            return this.ToActionResult(result);
        }
    }
}
