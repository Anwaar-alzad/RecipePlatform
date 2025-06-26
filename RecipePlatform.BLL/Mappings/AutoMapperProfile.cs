using AutoMapper;
using RecipePlatform.BLL.DTOs;
using RecipePlatform.Models;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.BLL.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //this creates auto mapping between DTOs and Models
            CreateMap<RecipeCategory, RecipeCategoryDto>().ReverseMap();
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<ApplicationUser, RegisterDto>().ReverseMap();
        }
    }

}
