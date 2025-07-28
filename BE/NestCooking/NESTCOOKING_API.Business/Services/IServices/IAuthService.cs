using NESTCOOKING_API.Business.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NESTCOOKING_API.Utility.StaticDetails;

namespace NESTCOOKING_API.Business.Services.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<(string, string)> GenerateEmailConfirmationTokenAsync(string email);
        Task<bool> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<string> LoginWithThirdParty(OAuth2RequestDTO oAuth2RequestDTO, ProviderLogin providerLogin);

    }
}
