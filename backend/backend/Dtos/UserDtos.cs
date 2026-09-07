using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record UserDto(
        int Id,
        string FirstName,
        string LastName,
        string Username,
        string Email
    );

    public record CreateUserDto(
        [Required, MaxLength(100)] string FirstName,
        [Required, MaxLength(100)] string LastName,
        [Required, MaxLength(50)] string Username,
        [Required, EmailAddress,MaxLength(256)] string Email,
        [Required, MinLength(8)] string Password
    );

    public record UpdateUserDto(
        [Required, MaxLength(100)] string FirstName,
        [Required, MaxLength(100)] string LastName,
        [Required, EmailAddress, MaxLength(256)] string Email
    );
}
