using Ardalis.Result;
using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class SavingService : ISavingService
    {
        private readonly FinanceDbContext _context;

        public SavingService(FinanceDbContext context) => _context = context;

        public Task<Result<SavingDto>> CreateAsync(int userId, CreateSavingDto newSaving)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> DeleteAsync(int userId, int id)
        {
            Saving? saving = await _context.Savings.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
            if (saving == null) return Result.NotFound("Požadovaný finančný cieľ neexistuje.");

            _context.Remove(saving);
            await _context.SaveChangesAsync();

            return Result.NoContent();
        }

        public async Task<Result<IEnumerable<SavingDto>>> GetAllAsync(int userId, CancellationToken cancellationToken)
        {
            IEnumerable<SavingDto> savings = await _context.Savings.Where(s => s.UserId == userId).Select(s => new SavingDto(s.Id, s.Name, s.Amount, s.Limit)).ToListAsync(cancellationToken);
            return Result<IEnumerable<SavingDto>>.Success(savings);
        }

        public async Task<Result<SavingDto>> GetByIdAsync(int userId, int id, CancellationToken cancellationToken)
        {
            Saving? saving = await _context.Savings.FirstOrDefaultAsync(s => s.UserId == userId && s.Id == id, cancellationToken);
            if (saving == null) return Result<SavingDto>.NotFound("Požadovaný finančný cieľ neexistuje.");

            return Result<SavingDto>.Success(new SavingDto(saving.Id, saving.Name, saving.Amount, saving.Limit));
        }

        public Task<Result<SavingDto>> UpdateAsync(int userId, int id, UpdateSavingDto saving)
        {
            throw new NotImplementedException();
        }
    }
}
