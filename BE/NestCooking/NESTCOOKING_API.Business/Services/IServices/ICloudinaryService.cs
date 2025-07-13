using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.Business.Services.IServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file, string path);
        Task<string> UploadVideoAsync(IFormFile file, string path);
    }
}
