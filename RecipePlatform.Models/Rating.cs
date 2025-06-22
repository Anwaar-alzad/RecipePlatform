using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.Models
{
    public class Rating : BaseEntity
    {
        public int Rate { get; set; }
        public string UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();


    }
}
