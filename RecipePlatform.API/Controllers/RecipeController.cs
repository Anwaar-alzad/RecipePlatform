using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces.Services;
using RecipePlatform.BLL.Interfaces;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IRatingService _ratingService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<RecipesController> _logger;

    public RecipesController(
        IRecipeService recipeService,
        IRatingService ratingService,
        IHttpContextAccessor accessor,
        ILogger<RecipesController> logger)
    {
        _recipeService = recipeService;
        _ratingService = ratingService;
        _httpContextAccessor = accessor;
        _logger = logger;
    }

    private string GetUserId() =>
        _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    /// <summary>
    /// Get all recipes
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var recipes = await _recipeService.GetAllAsync();
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all recipes");
            return StatusCode(500, "An error occurred while retrieving recipes");
        }
    }

    /// <summary>
    /// Get recipe by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
                return NotFound($"Recipe with ID {id} not found");

            return Ok(recipe);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving recipe with ID {RecipeId}", id);
            return StatusCode(500, "An error occurred while retrieving the recipe");
        }
    }

    /// <summary>
    /// Create a new recipe
    /// </summary>
    [HttpPost]
    [Authorize]
    //public async Task<IActionResult> Create([FromBody]CreateRecipeDto dto)
    //{
    //    try
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        var userId = GetUserId();
    //        if (string.IsNullOrEmpty(userId))
    //            return Unauthorized("User ID not found");

    //        var result = await _recipeService.CreateAsync(dto, userId);
    //        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error creating recipe");
    //        return StatusCode(500, "An error occurred while creating the recipe");
    //    }
    //}



    /// <summary>
    /// Create a new recipe
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateRecipeDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found");

            // Map CreateRecipeDto to RecipeDto
            var recipeDto = new RecipeDto
            {
                Title = dto.Title,
                Description = dto.Description,
                Ingredients = dto.Ingredients,
                Instructions = dto.Instructions,
                PrepTimeMinutes = dto.PrepTimeMinutes,
                CookTimeMinutes = dto.CookTimeMinutes,
                Servings = dto.Servings,
                Difficulty = dto.Difficulty
            };

            var result = await _recipeService.CreateAsync(recipeDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating recipe");
            return StatusCode(500, "An error occurred while creating the recipe");
        }
    }

    /// <summary>
    /// Update an existing recipe
    /// </summary>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] RecipeDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found");

            var success = await _recipeService.UpdateAsync(id, dto, userId);
            if (!success)
                return NotFound("Recipe not found or you don't have permission to update it");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating recipe with ID {RecipeId}", id);
            return StatusCode(500, "An error occurred while updating the recipe");
        }
    }

    /// <summary>
    /// Delete a recipe
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found");

            var success = await _recipeService.DeleteAsync(id, userId);
            if (!success)
                return NotFound("Recipe not found or you don't have permission to delete it");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting recipe with ID {RecipeId}", id);
            return StatusCode(500, "An error occurred while deleting the recipe");
        }
    }

    /// <summary>
    /// Rate a recipe
    /// </summary>
    [HttpPost("rate")]
    [Authorize]
    public async Task<IActionResult> Rate([FromBody] RatingDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found");

            await _ratingService.RateRecipeAsync(dto, userId);
            return Ok(new { message = "Recipe rated successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error rating recipe");
            return StatusCode(500, "An error occurred while rating the recipe");
        }
    }

    /// <summary>
    /// Search recipes by term
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term cannot be empty");

            var results = await _recipeService.SearchAsync(term.Trim());
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching recipes with term '{SearchTerm}'", term);
            return StatusCode(500, "An error occurred while searching recipes");
        }
    }

    /// <summary>
    /// Get recipes by current user
    /// </summary>
    [HttpGet("my-recipes")]
    [Authorize]
    public async Task<IActionResult> GetMyRecipes()
    {
        try
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found");

            var recipes = await _recipeService.GetRecipesByUserAsync(userId);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user recipes");
            return StatusCode(500, "An error occurred while retrieving your recipes");
        }
    }
}