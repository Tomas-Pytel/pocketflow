using Ardalis.Result;
using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly FinanceDbContext _context;

        public BudgetService(FinanceDbContext context) => _context = context;

        private async Task<BudgetStatusDto> BuildStatusAsync(Budget budget, CancellationToken cancellationToken)
        {
            (DateTime periodStart, DateTime periodEnd) = BudgetPeriodCalculator.GetCurrentPeriod(budget, DateTime.UtcNow);

            IQueryable<Transaction> query = _context.Transactions.Where(t =>
                t.UserId == budget.UserId &&
                t.TransactionType == TransactionType.Expense &&
                t.Date >= periodStart && t.Date <= periodEnd);

            if (budget.CategoryId.HasValue)
                query = query.Where(t => t.CategoryId == budget.CategoryId);

            decimal spent = await query.SumAsync(t => t.Amount, cancellationToken);

            return new BudgetStatusDto(
                budget.Id,
                budget.Limit,
                spent,
                budget.Limit - spent,
                periodStart,
                periodEnd,
                budget.CategoryId,
                budget.Category?.Name
            );
        }

        private async Task<bool> HasConflictingBudgetAsync(int userId, int? categoryId, BudgetPeriodType periodType, DateTime startDate, DateTime? endDate, int? excludeBudgetId = null)
        {
            IQueryable<Budget> query = _context.Budgets.Where(b =>
                b.UserId == userId &&
                b.CategoryId == categoryId);

            if (excludeBudgetId.HasValue)
                query = query.Where(b => b.Id != excludeBudgetId);

            if (periodType is BudgetPeriodType.Monthly or BudgetPeriodType.Yearly)
            {
                return await query.AnyAsync(b => b.PeriodType == periodType);
            }

            return await query.AnyAsync(b =>
                b.PeriodType == BudgetPeriodType.Custom &&
                b.StartDate <= (endDate ?? DateTime.MaxValue) &&
                (b.EndDate ?? DateTime.MaxValue) >= startDate);
        }

        public async Task<Result<BudgetDto>> CreateAsync(int userId, CreateBudgetDto newBudget)
        {
            if(newBudget.CategoryId.HasValue)
            {
                bool categoryExists = await _context.Categories.AnyAsync(c => c.Id == newBudget.CategoryId && c.UserId == userId);
                if (!categoryExists) return Result<BudgetDto>.Invalid(new List<ValidationError>
                {
                    new() { Identifier = nameof(newBudget.CategoryId), ErrorMessage = "Zadaná kategória neexistuje alebo ti nepatrí." }
                });
            }

            if(newBudget.BudgetPeriodType == BudgetPeriodType.Custom && !newBudget.EndDate.HasValue)
            {
                return Result<BudgetDto>.Invalid(new List<ValidationError>
                {
                    new() { Identifier = nameof(newBudget.EndDate), ErrorMessage = "Vlastná perióda vyžaduje dátum konca." }
                });
            }

            bool hasConflict = await HasConflictingBudgetAsync(userId, newBudget.CategoryId, newBudget.BudgetPeriodType, newBudget.StartDate, newBudget.EndDate);
            if (hasConflict)
                return Result<BudgetDto>.Conflict("Pre túto kategóriu a periódu už existuje prekrývajúci sa budget.");


            Budget budget = new Budget
            {
                UserId = userId,
                Limit = newBudget.Limit,
                PeriodType = newBudget.BudgetPeriodType,
                StartDate = newBudget.StartDate,
                EndDate = newBudget.BudgetPeriodType == BudgetPeriodType.Custom ? newBudget.EndDate : null,
                CategoryId = newBudget.CategoryId
            };

            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            string? categoryName = newBudget.CategoryId.HasValue
                ? await _context.Categories.Where(c => c.Id == newBudget.CategoryId).Select(c => c.Name).FirstOrDefaultAsync()
                : null;

            return Result<BudgetDto>.Created(new BudgetDto(budget.Id, budget.Limit, budget.PeriodType, budget.StartDate, budget.EndDate, budget.CategoryId, categoryName));
        }

        public async Task<Result> DeleteAsync(int userId, int id)
        {
            Budget? budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && b.Id == id);
            if (budget == null) return Result.NotFound("Požadovaný budget neexistuje.");

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();

            return Result.NoContent();
        }

        public async Task<Result<IEnumerable<BudgetStatusDto>>> GetAllAsync(int userId, CancellationToken cancellationToken)
        {
            List<Budget> budgets = await _context.Budgets.Where(b => b.UserId == userId).Include(b => b.Category).ToListAsync(cancellationToken);
            List<BudgetStatusDto> budgetsWithStatus = new List<BudgetStatusDto>();

            foreach (Budget b in budgets)
            {
                budgetsWithStatus.Add(await BuildStatusAsync(b, cancellationToken));
            }
            return Result<IEnumerable<BudgetStatusDto>>.Success(budgetsWithStatus);
        }

        public async Task<Result<BudgetStatusDto>> GetByIdAsync(int userId, int id, CancellationToken cancellationToken)
        {
            Budget? budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && b.Id == id, cancellationToken);
            if (budget == null) return Result.NotFound("Požadovaný budget neexistuje.");

            BudgetStatusDto budgetWithStatus = await BuildStatusAsync(budget, cancellationToken);
            return Result.Success(budgetWithStatus);
        }

        public async Task<Result<BudgetDto>> UpdateAsync(int userId, int id, UpdateBudgetDto budget)
        {
            Budget? existingBudget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && b.Id == id);
            if (existingBudget == null) return Result<BudgetDto>.NotFound("Požadovaný budget neexistuje.");

            if (existingBudget.PeriodType == BudgetPeriodType.Custom && budget.EndDate.HasValue)
            {
                bool hasConflict = await HasConflictingBudgetAsync(
                    userId, existingBudget.CategoryId, existingBudget.PeriodType,
                    existingBudget.StartDate, budget.EndDate, excludeBudgetId: id);

                if (hasConflict)
                    return Result<BudgetDto>.Conflict("Zmenená perióda by sa prekrývala s iným existujúcim budgetom.");
            }

            existingBudget.Limit = budget.Limit;

            if(existingBudget.PeriodType == BudgetPeriodType.Custom)
            {
                existingBudget.EndDate = budget.EndDate;
            }

            await _context.SaveChangesAsync();

            string? categoryName = existingBudget.CategoryId.HasValue
                ? await _context.Categories.Where(c => c.Id == existingBudget.CategoryId).Select(c => c.Name).FirstOrDefaultAsync()
                : null;

            return Result<BudgetDto>.Success(new BudgetDto(existingBudget.Id, existingBudget.Limit, existingBudget.PeriodType, existingBudget.StartDate, existingBudget.EndDate, existingBudget.CategoryId, categoryName));
        }
    }
}
