// BLL/Repositories/RecipeRepository.cs
using Microsoft.EntityFrameworkCore;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces.Repositories;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.BLL.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByUserIdAsync(string userId)
        {
            return await _context.Recipes
               .Where(r => r.UserId == userId)
               .ToListAsync();

        }

        public async Task<IEnumerable<Recipe>> SearchAsync(string term)
        {
            return await _context.Recipes
                          .Where(r => r.Title.Contains(term) || r.Description.Contains(term))
                          .ToListAsync();
        }
    }
}
