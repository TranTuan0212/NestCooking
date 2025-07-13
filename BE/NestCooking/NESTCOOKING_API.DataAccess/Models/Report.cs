using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class Report
    {
        [Key]
        public string Id { get; set; } = null!;
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string TargetId { get; set; }
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Content { get; set; } = null!;
        public List<string>? ImageUrls { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Response? Response { get; set; }
    }
}
