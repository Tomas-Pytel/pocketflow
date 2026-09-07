using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using backend.Dtos;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponseDto>> Register([FromBody] CreateUserDto newUser)
        {
            Result<TokenResponseDto> result = await _authService.RegisterAsync(newUser);
            return this.ToActionResult(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginDto dto)
        {
            Result<TokenResponseDto> result = await _authService.LoginAsync(dto);
            return this.ToActionResult(result);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponseDto>> Refresh(RefreshRequestDto dto)
        {
            Result<TokenResponseDto> result = await _authService.RefreshAsync(dto.RefreshToken);
            return this.ToActionResult(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(RefreshRequestDto dto)
        {
            Result result = await _authService.LogoutAsync(dto.RefreshToken);
            return this.ToActionResult(result);
        }
    }
}
