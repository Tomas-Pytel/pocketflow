using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record LoginDto([Required] string Email, [Required] string Password);
    public record RefreshRequestDto([Required] string RefreshToken);
    public record TokenResponseDto(string AccessToken, string RefreshToken);
}
