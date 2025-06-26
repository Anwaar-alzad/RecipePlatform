// BLL/Services/RecipeService.cs
using AutoMapper;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces;
using RecipePlatform.BLL.Interfaces.Repositories;
using RecipePlatform.BLL.Interfaces.Services;
using RecipePlatform.Models;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.BLL.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeDto>> GetAllAsync()
        {
            var recipes = await _recipeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        public async Task<RecipeDto> GetByIdAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<IEnumerable<RecipeDto>> GetByUserIdAsync(string userId)
        {
            var recipes = await _recipeRepository.GetRecipesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        public async Task<IEnumerable<RecipeDto>> SearchAsync(string term)
        {
            var recipes = await _recipeRepository.SearchAsync(term);
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        public async Task<RecipeDto> AddAsync(RecipeDto dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);
            recipe.UserId = dto.UserId; 

            await _recipeRepository.AddAsync(recipe);
            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<bool> UpdateAsync(RecipeDto dto)
        {
            var recipe = await _recipeRepository.GetByIdAsync(dto.Id);
            if (recipe == null) return false;

            _mapper.Map(dto, recipe);
            _recipeRepository.Update(recipe); // Changed from UpdateAsync to Update
            await _recipeRepository.SaveAsync(); // Added SaveAsync to persist changes
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe == null) return false;

            _recipeRepository.Delete(recipe); // Changed from DeleteAsync to Delete
            await _recipeRepository.SaveAsync(); // Added SaveAsync to persist changes
            return true;
        }
    }
}
