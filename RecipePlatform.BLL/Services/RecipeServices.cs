using AutoMapper;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces.Repositories;
using RecipePlatform.BLL.Interfaces;
using RecipePlatform.Models.RecipeModule;
using Microsoft.EntityFrameworkCore;

public class RecipeService : IRecipeService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Recipe> _repo;
    private readonly IRecipeService _recipeService;

    public RecipeService(IGenericRepository<Recipe> recipeRepo, IMapper mapper)
    {
        _repo = recipeRepo;
        _mapper = mapper;
    }

    //create recipe
    public async Task<RecipeDto> CreateAsync(RecipeDto dto, string userId)
    {
        var recipe = _mapper.Map<Recipe>(dto);
        recipe.UserId = userId;
        await _repo.AddAsync(recipe);
        await _repo.SaveAsync();
        return _mapper.Map<RecipeDto>(recipe);
    }
    //delete recipe
    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var recipe = await _repo.GetByIdAsync(id);
        if (recipe == null || recipe.UserId != userId)
            return false;

        _repo.Delete(recipe);
        await _repo.SaveAsync();
        return true;
    }

    //get all recipes by user
    public async Task<IEnumerable<RecipeDto>> GetRecipesByUserAsync(string userId)
    {
        var recipes = await _recipeService.GetRecipesByUserAsync(userId);
        return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
    }

    public async Task<IEnumerable<RecipeDto>> GetAllAsync()
    {
        var recipes = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
    }

    public async Task<RecipeDto> GetByIdAsync(int id)
    {
        var recipe = await _repo.GetByIdAsync(id);
        return _mapper.Map<RecipeDto>(recipe);
    }

    public async Task<IEnumerable<RecipeDto>> SearchAsync(string term)
    {

        //have access to all recpies
        var allRecipes = await _repo.GetAllAsync();
        var filtered = allRecipes.Where(r =>
            r.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            r.Description.Contains(term, StringComparison.OrdinalIgnoreCase));

        return _mapper.Map<IEnumerable<RecipeDto>>(filtered);
    }

    public async Task<bool> UpdateAsync(int id, RecipeDto dto, string userId)
    {
        var recipe = await _repo.GetByIdAsync(id);
        if (recipe == null || recipe.UserId != userId)
            return false;

        _mapper.Map(dto, recipe);
        _repo.Update(recipe);
        await _repo.SaveAsync();
        return true;
    }


   

  
}
