using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class Notification
    {
        public string Id { get; set; } = null!;
        public string? SenderId { get; set; } = null;
        public string? ReceiverId { get; set; } = null;
        public string NotificationType { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool IsSeen { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    }
}
