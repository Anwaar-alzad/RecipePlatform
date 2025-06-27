using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces;
using RecipePlatform.BLL.Interfaces.Repositories;
using RecipePlatform.BLL.Interfaces.Services;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.BLL.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IGenericRepository<Recipe> _repo;
        private readonly IMapper _mapper;

        public RecipeService(IGenericRepository<Recipe> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RecipeDto> CreateAsync(RecipeDto dto, string userId)
        {
            var entity = _mapper.Map<Recipe>(dto);
            entity.Id = 0;
            await _repo.AddAsync(entity);
            return _mapper.Map<RecipeDto>(entity); // Ensure a RecipeDto is returned
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Recipe not found");

            _repo.Delete(entity);
            return true; // Ensure a boolean value is returned
        }

        public async Task<IEnumerable<RecipeDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RecipeDto>>(entities);
        }

        public async Task<RecipeDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<RecipeDto>(entity);
        }
        // Get all recipes by a specific user
        //public async Task<IEnumerable<RecipeDto>> GetRecipesByUserAsync(string userId)
        //{
        //    var recipes = await _repo.FindAsync(r => r.CreatedByUserId == userId);
        //    return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        //}

        //// Search recipes by term (title or description)
        //public async Task<IEnumerable<RecipeDto>> SearchAsync(string term)
        //{
        //    var recipes = await _repo.FindAsync(r =>
        //        r.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
        //        r.Description.Contains(term, StringComparison.OrdinalIgnoreCase));

        //    return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        //}


        public async Task<bool> UpdateAsync(int id, RecipeDto dto, string userId)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Recipe not found");

            _mapper.Map(dto, entity);
            _repo.Update(entity);

            return true; // Ensure a boolean value is returned
        }
    }
}