using backend.Models;
using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{

    public record TransactionDto
    (
        int Id,
        decimal Amount,
        string? Description,
        TransactionType TransactionType,
        DateTime Date,
        int? CategoryId,
        string? CategoryName
    );

    public record CreateTransactionDto
    (
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Suma musí byť kladné číslo.")] decimal Amount,
        [Required] TransactionType TransactionType,
        [Required] DateTime Date,
        [MaxLength(500)] string? Description,
        int? CategoryId
    );

    public record UpdateTransactionDto(
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Suma musí byť kladné číslo.")] decimal Amount,
        [MaxLength(500)] string? Description,
        [Required] TransactionType TransactionType,
        [Required] DateTime Date,
        int? CategoryId
    );
}
