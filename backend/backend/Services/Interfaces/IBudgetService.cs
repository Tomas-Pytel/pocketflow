using Ardalis.Result;
using backend.Dtos;

namespace backend.Services.Interfaces
{
    public interface IBudgetService
    {
        public Task<Result<IEnumerable<BudgetStatusDto>>> GetAllAsync(int userId, CancellationToken cancellationToken);
        public Task<Result<BudgetStatusDto>> GetByIdAsync(int userId, int id, CancellationToken cancellationToken);
        public Task<Result<BudgetDto>> CreateAsync(int userId, CreateBudgetDto newBudget);
        public Task<Result<BudgetDto>> UpdateAsync(int userId, int id, UpdateBudgetDto budget);
        public Task<Result> DeleteAsync(int userId, int id);
    }
}
