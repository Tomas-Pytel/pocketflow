using Ardalis.Result;
using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly FinanceDbContext _context;

        public CategoryService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<Result<CategoryDto>> CreateAsync(int userId, CreateCategoryDto newCategory)
        {
            Category? category = await _context.Categories.Where(c => c.UserId == userId && c.Name == newCategory.Name).FirstOrDefaultAsync();
            if (category != null) return Result<CategoryDto>.Conflict("Kategória s daným názvom už existuje.");

            category = new Category
            {
                Name = newCategory.Name,
                ColorHex = newCategory.ColorHex,
                UserId = userId,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Result<CategoryDto>.Created(new CategoryDto(category.Id, category.Name, category.ColorHex));
        }

        public async Task<Result> DeleteAsync(int userId, int id)
        {
            Category? category = await _context.Categories.Where(c => c.UserId == userId && c.Id == id).FirstOrDefaultAsync();
            if (category == null) return Result.NotFound("Požadovaná kategória neexistuje.");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Result.NoContent();
        }

        public async Task<Result<IEnumerable<CategoryDto>>> GetAllAsync(int userId, CancellationToken cancellationToken)
        {
            IEnumerable<CategoryDto> categories = await _context.Categories.Where(c => c.UserId == userId).Select(c => new CategoryDto(c.Id, c.Name, c.ColorHex)).ToListAsync(cancellationToken);
            return Result<IEnumerable<CategoryDto>>.Success(categories);
        }

        public async Task<Result<CategoryDto>> GetByIdAsync(int userId, int id, CancellationToken cancellationToken)
        {
            Category? category = await _context.Categories.Where(c => c.UserId == userId && c.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (category == null) return Result<CategoryDto>.NotFound("Požadovaná kategória neexistuje.");

            return Result<CategoryDto>.Success(new CategoryDto(category.Id, category.Name, category.ColorHex));
        }

        public async Task<Result<CategoryDto>> UpdateAsync(int userId, int id, UpdateCategoryDto category)
        {
            Category? existingCategory = await _context.Categories.Where(c => c.UserId == userId && c.Id == id).FirstOrDefaultAsync();
            if (existingCategory == null) return Result<CategoryDto>.NotFound("Požadovaná kategória neexistuje.");

            bool nameTaken = await _context.Categories.AnyAsync(c => c.UserId == userId && c.Name == category.Name && c.Id != id);
            if (nameTaken) return Result<CategoryDto>.Conflict("Kategória s daným názvom už existuje.");

            existingCategory.Name = category.Name;
            existingCategory.ColorHex = category.ColorHex;

            await _context.SaveChangesAsync();

            return Result<CategoryDto>.Success(new CategoryDto(existingCategory.Id, existingCategory.Name, existingCategory.ColorHex));
        }
    }
}
