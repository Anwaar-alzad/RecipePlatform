using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        public  string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Recipe> Recipe { get; set; } = new List<Recipe>();



    }
}
