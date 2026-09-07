using Ardalis.Result;
using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly FinanceDbContext _context;

        public UserService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null) return Result.NotFound("Hľadaný používateľ neexistuje.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Result.NoContent();
        }

        public async Task<Result<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            IEnumerable<UserDto> users = await _context.Users.Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Username, u.Email)).ToListAsync(cancellationToken);
            return Result<IEnumerable<UserDto>>.Success(users);
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (user == null) return Result<UserDto>.NotFound("Hľadaný používateľ neexistuje.");

            return Result<UserDto>.Success(new UserDto(user.Id, user.FirstName, user.LastName, user.Username, user.Email));
        }

        public async Task<Result<UserDto>> UpdateAsync(int id, UpdateUserDto user)
        {
            User? existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null) return Result<UserDto>.NotFound("Hľadaný používateľ neexistuje.");

            bool emailTaken = await _context.Users.AnyAsync(u => u.Email == user.Email && u.Id != id);
            if (emailTaken) return Result<UserDto>.Conflict("Daný email už existuje.");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();

            return Result<UserDto>.Success(new UserDto(existingUser.Id, existingUser.FirstName, existingUser.LastName, existingUser.Username, existingUser.Email));
        }
    }
}
