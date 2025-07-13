using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Models
{
    public class UserConnection
    {
        public string UserId { get; set; } = null!;
        public string FollowingUserId { get; set; } = null!;
    }
}
