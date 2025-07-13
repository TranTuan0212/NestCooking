using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.DataAccess.Repositories.IRepositories
{
    public interface IOAuthRepository
    {
        Task<JObject> SignInWithGoogle(String accessToken);
        Task<JObject> SignInWithFacebook(String accessToken);
    }
}
