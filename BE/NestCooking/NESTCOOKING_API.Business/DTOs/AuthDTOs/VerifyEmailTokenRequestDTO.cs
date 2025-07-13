using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.Business.DTOs.AuthDTOs
{
    public class VerifyEmailTokenRequestDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
