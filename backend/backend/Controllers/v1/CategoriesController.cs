using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using backend.Dtos;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private int CurrentUserId => int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {
            Result<IEnumerable<CategoryDto>> result = await _categoryService.GetAllAsync(CurrentUserId, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
        {
            Result<CategoryDto> result = await _categoryService.GetByIdAsync(CurrentUserId, id, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto newCategory)
        {
            Result<CategoryDto> result = await _categoryService.CreateAsync(CurrentUserId, newCategory);

            if (!result.IsSuccess)
                return this.ToActionResult(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] UpdateCategoryDto category)
        {
            Result<CategoryDto> result = await _categoryService.UpdateAsync(CurrentUserId, id, category);
            return this.ToActionResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await _categoryService.DeleteAsync(CurrentUserId, id);
            return this.ToActionResult(result);
        }
    }
}
