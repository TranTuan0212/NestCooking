using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class RecipeReaction
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; }
        public string RecipeId { get; set; } = null!;
        public Recipe Recipe { get; set; }
        public string ReactionId { get; set; } = null!;
        public Reaction Reaction { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
