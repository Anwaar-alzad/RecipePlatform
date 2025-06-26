using RecipePlatform.BLL.DTOs;

namespace RecipePlatform.BLL.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDto>> GetAllAsync();
        Task<RecipeDto> GetByIdAsync(int id);
        Task<IEnumerable<RecipeDto>> GetByUserIdAsync(string userId);
        Task<IEnumerable<RecipeDto>> SearchAsync(string term);
        Task<RecipeDto> AddAsync(RecipeDto dto);
        Task<bool> UpdateAsync(RecipeDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
