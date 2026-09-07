using Ardalis.Result;
using backend.Dtos;

namespace backend.Services.Interfaces
{
    public interface IUserService
    {
        public Task<Result<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken);
        public Task<Result<UserDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<Result<UserDto>> UpdateAsync(int id, UpdateUserDto user);
        public Task<Result> DeleteAsync(int id);
    }
}
