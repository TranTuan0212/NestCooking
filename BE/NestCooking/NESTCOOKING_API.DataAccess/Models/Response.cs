using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class Response
    {
        [Key]
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
