using Ardalis.Result;
using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly FinanceDbContext _context;
        public TransactionService(FinanceDbContext context) => _context = context;

        public async Task<Result<TransactionDto>> CreateAsync(int userId, CreateTransactionDto newTransaction)
        {
            if (newTransaction.CategoryId.HasValue)
            {
                bool categoryExists = await _context.Categories.AnyAsync(c => c.UserId == userId && c.Id == newTransaction.CategoryId);
                if (!categoryExists) return Result<TransactionDto>.Invalid(new List<ValidationError>
                {
                    new() { Identifier = nameof(newTransaction.CategoryId), ErrorMessage = "Zadaná kategória neexistuje alebo ti nepatrí." }
                });
            }

            Transaction transaction = new Transaction
            {
                UserId = userId,
                Amount = newTransaction.Amount,
                Date = newTransaction.Date,
                Description = newTransaction.Description,
                TransactionType = newTransaction.TransactionType,
                CategoryId = newTransaction.CategoryId,
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            string? categoryName = newTransaction.CategoryId.HasValue
            ? await _context.Categories.Where(c => c.Id == newTransaction.CategoryId).Select(c => c.Name).FirstOrDefaultAsync()
            : null;

            return Result<TransactionDto>.Created(new TransactionDto(transaction.Id, transaction.Amount, transaction.Description, transaction.TransactionType, transaction.Date, transaction.CategoryId, categoryName));
        }

        public async Task<Result> DeleteAsync(int userId, int id)
        {
            Transaction? transaction = await _context.Transactions.Where(t => t.UserId == userId && t.Id == id).FirstOrDefaultAsync();
            if (transaction == null) return Result.NotFound("Daná transakcia neexistuje.");

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return Result.NoContent();
        }

        public async Task<Result<IEnumerable<TransactionDto>>> GetAllAsync(int userId, CancellationToken cancellationToken)
        {
            IEnumerable<TransactionDto> transactions = await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .Select(t => new TransactionDto(t.Id, t.Amount, t.Description, t.TransactionType, t.Date, t.CategoryId, t.Category != null ? t.Category.Name : null))
                .ToListAsync(cancellationToken);
            return Result<IEnumerable<TransactionDto>>.Success(transactions);
        }

        public async Task<Result<TransactionDto>> GetByIdAsync(int userId, int id, CancellationToken cancellationToken)
        {
            Transaction? transaction = await _context.Transactions
                .Where(t => t.UserId == userId && t.Id == id)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(cancellationToken);
            if (transaction == null) return Result<TransactionDto>.NotFound("Daná transakcia neexistuje.");

            return Result<TransactionDto>.Success(new TransactionDto(transaction.Id, transaction.Amount, transaction.Description, transaction.TransactionType, transaction.Date, transaction.CategoryId, transaction.Category?.Name));
        }

        public async Task<Result<TransactionDto>> UpdateAsync(int userId, int id, UpdateTransactionDto transaction)
        {
            Transaction? existingTransaction = await _context.Transactions
                .Where(t => t.UserId == userId && t.Id == id)
                .FirstOrDefaultAsync();
            if (existingTransaction == null) return Result<TransactionDto>.NotFound("Daná transakcia neexistuje.");

            if (transaction.CategoryId.HasValue)
            {
                bool categoryExists = await _context.Categories.AnyAsync(c => c.Id == transaction.CategoryId && c.UserId == userId);
                if (!categoryExists) return Result<TransactionDto>.Invalid(new List<ValidationError>
                {
                    new() { Identifier = nameof(transaction.CategoryId), ErrorMessage = "Zadaná kategória neexistuje alebo ti nepatrí." }
                });
            }

            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Description = transaction.Description;
            existingTransaction.Date = transaction.Date;
            existingTransaction.TransactionType = transaction.TransactionType;
            existingTransaction.CategoryId = transaction.CategoryId;

            await _context.SaveChangesAsync();

            string? categoryName = existingTransaction.CategoryId.HasValue
                ? await _context.Categories.Where(c => c.Id == existingTransaction.CategoryId).Select(c => c.Name).FirstOrDefaultAsync()
                : null;

            return Result<TransactionDto>.Success(new TransactionDto(existingTransaction.Id, existingTransaction.Amount, existingTransaction.Description, existingTransaction.TransactionType, existingTransaction.Date, existingTransaction.CategoryId, categoryName));
        }
    }
}
