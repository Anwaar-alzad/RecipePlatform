using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.BLL.DTOs;

namespace RecipePlatform.BLL.Interfaces.Services
{
    public interface IRatingService
    {
        Task RateRecipeAsync(RatingDto dto, string userId);

    }
}
