// API/Controllers/RecipeController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces;
using RecipePlatform.BLL.Interfaces.Services;

namespace RecipePlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/Recipe
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var recipes = await _recipeService.GetAllAsync();
            return Ok(recipes);
        }

        // GET: api/Recipe/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }

        // GET: api/Recipe/search?term=pizza
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            var results = await _recipeService.SearchAsync(term);
            return Ok(results);
        }

        // GET: api/Recipe/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var recipes = await _recipeService.GetByUserIdAsync(userId);
            return Ok(recipes);
        }

        // POST: api/Recipe
        [HttpPost]
        [Authorize] // optional: only logged-in users can post
        public async Task<IActionResult> Create([FromBody] RecipeDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            dto.UserId = userId; // ربط الـ UserId

            var created = await _recipeService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Recipe/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] RecipeDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Mismatched ID");

            var updated = await _recipeService.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Recipe/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _recipeService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
