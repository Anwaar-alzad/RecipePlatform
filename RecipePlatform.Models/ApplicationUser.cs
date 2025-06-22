using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipePlatform.Models
{
    public class ApplicationUser 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();


    }
}
