using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.Business.DTOs.EmailDTOs
{
    public class EmailRequestDTO
    {
        public string From { get; set; } = null;
        public string SmtpServer { get; set; } = null;
        public int Port { get; set; }
        public string UserName { get; set; } = null;
        public string Password { get; set; } = null;


    }
}
