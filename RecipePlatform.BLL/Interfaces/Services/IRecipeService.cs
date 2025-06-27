using RecipePlatform.BLL.DTOs;

namespace RecipePlatform.BLL.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDto>> GetAllAsync();
        //this should be getRecipeByName
        Task<RecipeDto> GetByIdAsync(int id);
        //Task<IEnumerable<RecipeDto>> GetRecipesByUserAsync(string userId);
        //Task<IEnumerable<RecipeDto>> SearchAsync(string term);
        Task<RecipeDto> CreateAsync(RecipeDto dto, string userId);
        Task<bool> UpdateAsync(int id, RecipeDto dto, string userId);
        Task<bool> DeleteAsync(int id, string userId);

    }
}
