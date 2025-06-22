using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.Models.RecipeModule.Enums;

namespace RecipePlatform.Models.RecipeModule
{
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public int PrepTimeMinutes { get; set; }
        public int CookTimeMinutes { get; set; }
        public int Servings { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public DateTime CreatedDate { get; set; }
       
        public string UserId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual RecipeCategory Category { get; set; }
        //many ingrediants in one recipe
        //public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();


    }
}
