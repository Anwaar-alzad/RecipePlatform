using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.BLL.Interfaces.Services;

namespace RecipePlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRecipeCategoryService _service;

        public CategoryController(IRecipeCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllGategoriesAsync();

            if(categories == null || !categories.Any())
            {
                return NotFound("No Categories found");
            }
            return Ok(categories);
        }
            //=> Ok(await _service.GetAllGategoriesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecipeCategoryDto dto)
        {
            await _service.AddAsync(dto);
            return Ok();
        }


        //only this doesn't work
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RecipeCategoryDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _service.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
