
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using backend.Dtos;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        Result<IEnumerable<UserDto>> result = await _userService.GetAllAsync(cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetById(int id, CancellationToken cancellationToken)
    {
        int currentUserId = int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
        if (currentUserId != id) return Forbid();

        Result<UserDto> result = await _userService.GetByIdAsync(id, cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        int currentUserId = int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
        if (currentUserId != id) return Forbid();

        Result<UserDto> result = await _userService.UpdateAsync(id, updateUserDto);
        return this.ToActionResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        int currentUserId = int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
        if (currentUserId != id) return Forbid();

        Result result = await _userService.DeleteAsync(id);
        return this.ToActionResult(result);
    }
}
