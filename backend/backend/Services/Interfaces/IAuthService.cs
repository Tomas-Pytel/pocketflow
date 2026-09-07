using Ardalis.Result;
using backend.Dtos;

namespace backend.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Result<TokenResponseDto>> LoginAsync(LoginDto loginDto);
        public Task<Result<TokenResponseDto>> RefreshAsync(string refreshToken);
        public Task<Result> LogoutAsync(string refreshToken);
        public Task<Result<TokenResponseDto>> RegisterAsync(CreateUserDto newUser);
    }
}
