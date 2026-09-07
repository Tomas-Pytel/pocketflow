using Ardalis.Result;
using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly FinanceDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthService(FinanceDbContext context, IPasswordHasher<User> passwordHasher, ITokenService tokenService, IConfiguration config)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _configuration = config;
        }

        private async Task<TokenResponseDto> IssueTokensAsync(User user)
        {
            string accessToken = _tokenService.GenerateAccessToken(user);
            string refreshToken = _tokenService.GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                TokenHash = _tokenService.HashToken(refreshToken),
                ExpiresAt = DateTime.UtcNow.AddDays(double.Parse(_configuration["Jwt:RefreshTokenExpiryDays"]!))
            });
            await _context.SaveChangesAsync();

            return new TokenResponseDto(accessToken, refreshToken);
        }

        private async Task RevokeAllUserTokensAsync(int userId)
        {
            var activeTokens = await _context.RefreshTokens
                .Where(t => t.UserId == userId && t.RevokedAt == null)
                .ToListAsync();

            foreach (var t in activeTokens) t.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<Result<TokenResponseDto>> LoginAsync(LoginDto loginDto)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password) == PasswordVerificationResult.Failed)
                return Result<TokenResponseDto>.Unauthorized("Nesprávny email alebo heslo.");

            return Result<TokenResponseDto>.Success(await IssueTokensAsync(user));
        }

        public async Task<Result> LogoutAsync(string refreshToken)
        {
            string hash = _tokenService.HashToken(refreshToken);
            RefreshToken? stored = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == hash);

            if (stored != null)
            {
                stored.RevokedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return Result.NoContent();
        }

        public async Task<Result<TokenResponseDto>> RefreshAsync(string refreshToken)
        {
            string incomingHash = _tokenService.HashToken(refreshToken);
            RefreshToken? stored = await _context.RefreshTokens.Include(t => t.User).FirstOrDefaultAsync(t => t.TokenHash == incomingHash);

            if (stored == null) return Result<TokenResponseDto>.Unauthorized("Neplatný refresh token.");

            if(!stored.IsActive)
            {
                if (stored.RevokedAt != null)
                    await RevokeAllUserTokensAsync(stored.UserId);

                return Result<TokenResponseDto>.Unauthorized("Refresh token už nie je platný.");
            }

            stored.RevokedAt = DateTime.UtcNow;
            var newTokens = await IssueTokensAsync(stored.User);
            stored.ReplacedByTokenHash = _tokenService.HashToken(newTokens.RefreshToken);
            await _context.SaveChangesAsync();

            return Result<TokenResponseDto>.Success(newTokens);
        }

        public async Task<Result<TokenResponseDto>> RegisterAsync(CreateUserDto newUser)
        {
            bool exists = await _context.Users.AnyAsync(u => u.Email == newUser.Email || u.Username == newUser.Username);
            if (exists) return Result<TokenResponseDto>.Conflict("Username alebo Email je už registrovaný.");

            User user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Username = newUser.Username,
                Email = newUser.Email,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, newUser.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Result<TokenResponseDto>.Success(await IssueTokensAsync(user));
        }
    }
}
