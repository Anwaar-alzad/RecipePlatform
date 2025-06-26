// BLL/Interfaces/Repositories/IRecipeRepository.cs
using RecipePlatform.BLL.DTOs;
using RecipePlatform.Models;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.BLL.Interfaces.Repositories
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetRecipesByUserIdAsync(string userId);
        Task<IEnumerable<Recipe>> SearchAsync(string term);
    }
}
