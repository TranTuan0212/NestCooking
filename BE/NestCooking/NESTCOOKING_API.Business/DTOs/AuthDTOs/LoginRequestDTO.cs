using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.Business.DTOs.AuthDTOs
{
    public class LoginRequestDTO
    {
        [DefaultValue("test")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DefaultValue("Test123@")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
