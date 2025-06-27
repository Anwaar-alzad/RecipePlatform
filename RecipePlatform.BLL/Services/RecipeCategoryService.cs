using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces.Repositories;
using RecipePlatform.BLL.Interfaces.Services;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.BLL.Services
{
    public class RecipeCategoryService : IRecipeCategoryService
    {
        private readonly IGenericRepository<RecipeCategory> _repo;
        private readonly IMapper _mapper;

        public RecipeCategoryService(IGenericRepository<RecipeCategory> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        // create category
        public async Task AddAsync(RecipeCategoryDto dto)
        {
            var entity = _mapper.Map<RecipeCategory>(dto);
            entity.Id = 0;

            await _repo.AddAsync(entity);
        }

        // delete category  
        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Category not found");
            _repo.Delete(entity);
        }
        public async Task<IEnumerable<RecipeCategoryDto>> GetAllGategoriesAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RecipeCategoryDto>>(entities);
        }
        public async Task<RecipeCategoryDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<RecipeCategoryDto>(entity);
        }
        public async Task UpdateAsync(RecipeCategoryDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null) throw new KeyNotFoundException("Category not found");
            _mapper.Map(dto, entity);
            _repo.Update(entity);
        }
        public async Task SaveAsync()
        {
            await _repo.SaveAsync();
        }
    }
}
