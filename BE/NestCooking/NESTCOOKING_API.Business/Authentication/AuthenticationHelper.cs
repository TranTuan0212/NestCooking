using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NESTCOOKING_API.Business.Authentication
{
    public class AuthenticationHelper
    {
        public static string GetUserIdFromContext(HttpContext context)
        {
           return context.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
