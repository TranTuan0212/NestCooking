using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class CommentReaction
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; }
        public string CommentId { get; set; } = null!;
        public Comment Comment { get; set; }
        public string ReactionId { get; set; } = null!;
        public Reaction Reaction { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
