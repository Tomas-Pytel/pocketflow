using Ardalis.Result;
using backend.Dtos;

namespace backend.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<Result<IEnumerable<TransactionDto>>> GetAllAsync(int userId, CancellationToken cancellationToken);
        public Task<Result<TransactionDto>> GetByIdAsync(int userId, int id, CancellationToken cancellationToken);
        public Task<Result<TransactionDto>> CreateAsync(int userId, CreateTransactionDto newTransaction);
        public Task<Result<TransactionDto>> UpdateAsync(int userId, int id, UpdateTransactionDto transaction);
        public Task<Result> DeleteAsync(int userId, int id);
    }
}
