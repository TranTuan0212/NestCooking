﻿using NESTCOOKING_API.Business.DTOs.EmailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.Business.Services.IServices
{
    public interface IEmailService
    {
        void SendEmail(EmailResponseDTO message);
    }
}
