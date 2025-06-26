using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.BLL.DTOs;

namespace RecipePlatform.BLL.Interfaces.Services
{
    public interface IRecipeCategoryService 
    {
        Task<IEnumerable<RecipeCategoryDto>> GetAllGategoriesAsync();
        Task<RecipeCategoryDto?> GetByIdAsync(int id);
        Task AddAsync(RecipeCategoryDto dto);
        Task UpdateAsync(RecipeCategoryDto dto);
        Task DeleteAsync(int id);
    }
}
