using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces;
using RecipePlatform.BLL.Interfaces.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipePlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/recipe
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recipes = await _recipeService.GetAllAsync();
            return Ok(recipes);
        }

        // GET: api/recipe/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        // POST: api/recipe
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecipeDto dto)
        {
           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var createdRecipe = await _recipeService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = createdRecipe.Id }, createdRecipe);
        }

        // PUT: api/recipe/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RecipeDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _recipeService.UpdateAsync(id, dto, userId);
            return result ? NoContent() : BadRequest("Failed to update recipe.");
        }

        // DELETE: api/recipe/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _recipeService.DeleteAsync(id, userId);
            return result ? NoContent() : NotFound("Recipe not found.");
        }
    }
}
