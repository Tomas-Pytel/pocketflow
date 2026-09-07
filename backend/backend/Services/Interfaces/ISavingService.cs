using Ardalis.Result;
using backend.Dtos;

namespace backend.Services.Interfaces
{
    public interface ISavingService
    {
        public Task<Result<IEnumerable<SavingDto>>> GetAllAsync(int userId, CancellationToken cancellationToken);
        public Task<Result<SavingDto>> GetByIdAsync(int userId, int id, CancellationToken cancellationToken);
        public Task<Result<SavingDto>> CreateAsync(int userId, CreateSavingDto newSaving);
        public Task<Result<SavingDto>> UpdateAsync(int userId, int id, UpdateSavingDto saving);
        public Task<Result> DeleteAsync(int userId, int id);
    }
}
