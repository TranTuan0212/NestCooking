using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class CategoryRecipe
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
