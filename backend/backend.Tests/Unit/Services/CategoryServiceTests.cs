using Ardalis.Result;
using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Tests.Unit.Services
{
    public class CategoryServiceTests : IDisposable
    {
        private readonly FinanceDbContext _context;
        private readonly CategoryService _sut;
    
        public CategoryServiceTests()
        {
            var options = new DbContextOptionsBuilder<FinanceDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new FinanceDbContext(options);
            _sut = new CategoryService(_context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOnlyCategoriesBelongingToUser()
        {
            // Arrange
            _context.Categories.Add(new Category { Name= "Jedlo", ColorHex= "#FF0000", UserId= 1 });
            _context.Categories.Add(new Category { Name= "Oblecenie", ColorHex= "#FFEE00", UserId= 1 });
            _context.Categories.Add(new Category { Name = "Cudzia kategória", ColorHex = "#0000FF", UserId = 2 });
            await _context.SaveChangesAsync(TestContext.Current.CancellationToken);
            
            // Act
            var result = await _sut.GetAllAsync(userId: 1, TestContext.Current.CancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);
            result.Value.Should().NotContain(c => c.Name == "Cudzia kategória");
        }

        [Fact]
        public async Task GetAllAsync_WithNoCategories_ReturnsEmptyList()
        {
            // Act
            var result = await _sut.GetAllAsync(userId: 1, TestContext.Current.CancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingIdForCorrectUser_ReturnsCategory()
        {
            // Arrange
            Category category = new Category {Id= 2, UserId = 1, Name = "Jedlo", ColorHex = "#FF0000" };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

            // Act
            var result = await _sut.GetByIdAsync(userId: 1, 2, TestContext.Current.CancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be("Jedlo");
            result.Value.ColorHex.Should().Be("#FF0000");
        }

        [Fact]
        public async Task GetByIdAsync_WithNonexistingId_ReturnsNotFound()
        {
            // Arrange
            Category category = new Category { Id = 2, UserId = 1, Name = "Jedlo", ColorHex = "#FF0000" };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

            // Act
            var result = await _sut.GetByIdAsync(userId: 1, 1, TestContext.Current.CancellationToken);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Fact]
        public async Task GetByIdAsync_WithIdBelongingToDifferentUser_ReturnsNotFound()
        {
            // Arrange
            Category category = new Category { Id = 2, UserId = 1, Name = "Jedlo", ColorHex = "#FF0000" };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

            // Act
            var result = await _sut.GetByIdAsync(userId: 2, 2, TestContext.Current.CancellationToken);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Fact]
        public async Task CreateAsync_WithNewName_ReturnsCreatedCategory()
        {
            // Arrange
            CreateCategoryDto dto = new CreateCategoryDto("Jedlo", "#FF0000");

            // Act
            var result = await _sut.CreateAsync(userId: 1, dto);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be("Jedlo");
            result.Value.ColorHex.Should().Be("#FF0000");
        }

        [Fact]
        public async Task CreateAsync_ActuallySavesCategoryToDatabase()
        {
            // Arrange
            CreateCategoryDto dto = new CreateCategoryDto("Jedlo", "#FF0000");

            // Act
            Result<CategoryDto> result = await _sut.CreateAsync(userId: 1, dto);
            Category? saved = await _context.Categories.FirstOrDefaultAsync(c => c.Id == result.Value.Id, TestContext.Current.CancellationToken);
            
            // Assert
            saved.Should().NotBeNull();
            saved.UserId.Should().Be(1);
        }

        [Fact]
        public async Task CreateAsync_WithDuplicateNameForSameUser_ReturnsConflict()
        {
            // Arrange
            _context.Categories.Add(new Category { Name = "Jedlo", ColorHex = "#FF0000", UserId = 1 });
            await _context.SaveChangesAsync(TestContext.Current.CancellationToken);
            var dto = new CreateCategoryDto("Jedlo", "#00FF00");

            // Act
            var result = await _sut.CreateAsync(userId: 1, dto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultStatus.Conflict);
        }

        [Fact]
        public async Task UpdateAsync_WithValidData_UpdatesAndReturnsCategory() { }
        [Fact]
        public async Task UpdateAsync_WithNonexistentId_ReturnsNotFound() { }
        [Fact]
        public async Task UpdateAsync_WithIdBelongingToDifferentUser_ReturnsNotFound() { }
        [Fact]
        public async Task UpdateAsync_WithNameAlreadyUsedByAnotherCategory_ReturnsConflict() { }
        [Fact]
        public async Task UpdateAsync_KeepingSameName_DoesNotReturnConflict() { }

        [Fact]
        public async Task DeleteAsync_WithExistingId_RemovesCategoryFromDatabase()
        {
            // Arrange
            _context.Categories.Add(new Category { Id = 1, UserId = 1, Name = "Jedlo", ColorHex = "#FF0000" });
            await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

            // Act
            Result result = await _sut.DeleteAsync(userId: 1, id: 1);
            Category? cat = await _context.Categories.FirstOrDefaultAsync(c => c.Id == 1 && c.UserId == 1, TestContext.Current.CancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
            cat.Should().BeNull();
        }
        [Fact]
        public async Task DeleteAsync_WithNonexistentId_ReturnsNotFound() 
        {
            // Arrange
            _context.Categories.Add(new Category { Id = 1, UserId = 1, Name = "Jedlo", ColorHex = "#FF0000" });
            await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

            // Act
            Result result = await _sut.DeleteAsync(userId: 1, id: 2);
            Category? cat = await _context.Categories.FirstOrDefaultAsync(c => c.Id == 1 && c.UserId == 1, TestContext.Current.CancellationToken);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultStatus.NotFound);
            cat.Should().NotBeNull();
            cat.Name.Should().Be("Jedlo");
        }
        [Fact]
        public async Task DeleteAsync_WithIdBelongingToDifferentUser_ReturnsNotFound() 
        {
            // Arrange
            _context.Categories.Add(new Category { Id = 1, UserId = 1, Name = "Jedlo", ColorHex = "#FF0000" });
            await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

            // Act
            Result result = await _sut.DeleteAsync(userId: 2, id: 1);
            Category? cat = await _context.Categories.FirstOrDefaultAsync(c => c.Id == 1 && c.UserId == 1, TestContext.Current.CancellationToken);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultStatus.NotFound);
            cat.Should().NotBeNull();
            cat.Name.Should().Be("Jedlo");
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
