using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class CommentHierarchy
    {
        [Key]
        public string Id { get; set; } = null!;
        public Comment ParrentComment { get; set; }
        public Comment ChildComment { get; set; }
    }
}
