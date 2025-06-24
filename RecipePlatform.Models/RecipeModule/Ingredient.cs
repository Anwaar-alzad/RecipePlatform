using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RecipePlatform.Models.RecipeModule
{
    //I might use it later
    public abstract class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        //to link between ingrendient and recipe
        //One recipe has many ingredients
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
