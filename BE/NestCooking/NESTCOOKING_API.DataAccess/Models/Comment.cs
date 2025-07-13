using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class Comment
    {
        [Key]
        public string CommentId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public User User { get; set; }
        public string RecipeId { get; set; } = null!;
        public Recipe Recipe { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ParentCommentId { get; set; }
        [ForeignKey("ParentCommentId")]
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> ChildComments { get; set; }
    }
}
