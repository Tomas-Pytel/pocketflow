using backend.Models;

namespace backend.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
        public string HashToken(string token);
    }
}
