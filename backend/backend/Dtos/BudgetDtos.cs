using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record BudgetDto
    (
        int Id,
        decimal Limit,
        BudgetPeriodType PeriodType,
        DateTime StartDate,
        DateTime? EndDate,
        int? CategoryId,
        string? CategoryName
    );

    public record BudgetStatusDto
    (
        int Id,
        decimal Limit,
        decimal Spent,
        decimal Remaining,
        DateTime PeriodStart,
        DateTime PeriodEnd,
        int? CategoryId,
        string? CategoryName
    );

    public record CreateBudgetDto
    (
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Suma musí byť kladné číslo.")] decimal Limit,
        [Required] BudgetPeriodType BudgetPeriodType,
        [Required] DateTime StartDate,
        DateTime? EndDate,
        int? CategoryId
    );

    public record UpdateBudgetDto
    (
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Suma musí byť kladné číslo.")] decimal Limit,
        DateTime? EndDate
    );
}
