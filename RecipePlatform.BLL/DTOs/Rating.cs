using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipePlatform.BLL.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int RecipeId { get; set; }
    }
}
