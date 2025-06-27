using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.Models;
using RecipePlatform.Models.RecipeModule.Enums;

namespace RecipePlatform.BLL.DTOs
{
    public class RecipeDto : BaseEntity
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public int PrepTimeMinutes { get; set; }
        public int CookTimeMinutes { get; set; }
        public int Servings { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        //public string? UserId { get; set; } 



        //who created this recipe?
        //this is not used in the front end, but it is used
        //in the back end to get the user who created this recipe

        //in case of using JWT this is no longer needed
        //public string UserId { get; set; }
        //under which category this recipe is?
        //public int CategoryId { get; set; }



        public int TotalTimeMinutes => PrepTimeMinutes + CookTimeMinutes;
        public string UserId { get; set; } // For response purposes
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public double? AverageRating { get; set; }
        public int? RatingCount { get; set; }

    }
}
