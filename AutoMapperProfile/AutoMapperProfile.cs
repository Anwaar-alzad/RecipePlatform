using AutoMapper;
using RecipePlatform.API.DTOs;
using RecipePlatform.Models;

namespace RecipePlatform.API.AutoMapperProfile
{ 
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, RegisterDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
