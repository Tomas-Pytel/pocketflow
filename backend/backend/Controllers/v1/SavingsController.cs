using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using backend.Dtos;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SavingsController : ControllerBase
    {
        private readonly ISavingService _savingService;
        private int UserId => int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

        public SavingsController(ISavingService savingService)
        {
            _savingService = savingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavingDto>>> GetAll(CancellationToken cancellationToken) 
        {
            Result<IEnumerable<SavingDto>> result = await _savingService.GetAllAsync(UserId, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SavingDto>> GetById(int id, CancellationToken cancellationToken)
        {
            Result<SavingDto> result = await _savingService.GetByIdAsync(UserId, id, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<SavingDto>> Create([FromBody] CreateSavingDto dto) 
        {
            Result<SavingDto> result = await _savingService.CreateAsync(UserId, dto);
            if (!result.IsSuccess) return this.ToActionResult(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SavingDto>> Update(int id, [FromBody] UpdateSavingDto dto)
        {
            Result<SavingDto> result = await _savingService.UpdateAsync(UserId, id, dto);
            return this.ToActionResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await _savingService.DeleteAsync(UserId, id);
            return this.ToActionResult(result);
        }
    }
}
