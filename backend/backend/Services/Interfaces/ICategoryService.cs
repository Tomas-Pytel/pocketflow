using Ardalis.Result;
using backend.Dtos;

namespace backend.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<Result<IEnumerable<CategoryDto>>> GetAllAsync(int userId, CancellationToken cancellationToken);
        public Task<Result<CategoryDto>> GetByIdAsync(int userId, int id, CancellationToken cancellationToken);
        public Task<Result<CategoryDto>> CreateAsync(int userId, CreateCategoryDto newCategory);
        public Task<Result<CategoryDto>> UpdateAsync(int userId, int id, UpdateCategoryDto category);
        public Task<Result> DeleteAsync(int userId, int id);
    }
}
