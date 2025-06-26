using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.Models
{
        public class Rating : BaseEntity
        {
            public int Rate { get; set; }

            //one user can have many ratings 
            public string UserId { get; set; }
            public virtual ApplicationUser User { get; set; }
            public int RecipeId { get; set; }
            public virtual Recipe Recipe { get; set; }


        

        }
}
