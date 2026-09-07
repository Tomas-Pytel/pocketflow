using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record CategoryDto(int Id, string Name, string ColorHex);
    public record CreateCategoryDto
    (
        [Required, MaxLength(50)]string Name, 
        [Required, MaxLength(7), RegularExpression(@"^#[0-9A-Fa-f]{6}$")] string ColorHex
    );
    public record UpdateCategoryDto
    (
        [Required, MaxLength(50)] string Name,
        [Required, MaxLength(7), RegularExpression(@"^#[0-9A-Fa-f]{6}$")] string ColorHex
    );
}
