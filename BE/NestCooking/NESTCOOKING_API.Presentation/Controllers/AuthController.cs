using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NESTCOOKING_API.Business.DTOs;
using NESTCOOKING_API.Business.DTOs.AuthDTOs;
using NESTCOOKING_API.Business.DTOs.EmailDTOs;
using NESTCOOKING_API.Business.Exceptions;
using NESTCOOKING_API.Business.Services.IServices;
using NESTCOOKING_API.Utility;
using static NESTCOOKING_API.Utility.StaticDetails;

namespace NESTCOOKING_API.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService, IEmailService emailService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _emailService = emailService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseDTO.BadRequest(message: "Error in request!"));
            }
            try
            {
                var loginResponse = await _authService.Login(model);

                if (loginResponse == null)
                {
                    return BadRequest(ResponseDTO.BadRequest(message: AppString.IncorrectCredentialsLoginErrorMessage));
                }
                else if (string.IsNullOrEmpty(loginResponse.AccessToken))
                {
                    return BadRequest(ResponseDTO.BadRequest(message: AppString.AccountLockedOutLoginErrorMessage));
                }
                return Ok(ResponseDTO.Accept(result: loginResponse));
            }
            catch (EmailNotConfirmedException exception)
            {
                var (email, token) = await _authService.GenerateEmailConfirmationTokenAsync(model.UserName);

                //var emailConfirmationLink = Url.Action(nameof(VerifyEmailConfirmation), "auth", new { token, email = model.Email }, Request.Scheme);
                var emailConfirmationLink = $"{StaticDetails.FE_URL}/verify-email?token={token}&email={email}";

                _emailService.SendEmail(new EmailResponseDTO(
                    to: new string[] { email },
                    subject: AppString.ResendEmailConfirmationSubjectEmail,
                    content: AppString.ResendEmailConfirmationContentEmail(emailConfirmationLink)
                ));

                return BadRequest(ResponseDTO.BadRequest(message: exception.Message));
            }
            catch (Exception error)
            {
                return BadRequest(ResponseDTO.BadRequest(message: error.Message));
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {

            if (!Validation.CheckEmailValid(model.Email))
            {
                return BadRequest(ResponseDTO.BadRequest(message: AppString.InvalidEmailErrorMessage));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseDTO.BadRequest(message: AppString.RequestErrorMessage));
            }

            try
            {
                var isRegisted = await _authService.Register(model);
                if (isRegisted)
                {
                    var (email, token) = await _authService.GenerateEmailConfirmationTokenAsync(model.Email);

                    //var emailConfirmationLink = Url.Action(nameof(VerifyEmailConfirmation), "auth", new { token, email = model.Email }, Request.Scheme);
                    var emailConfirmationLink = $"{StaticDetails.FE_URL}/verify-email?token={token}&email={email}";

                    _emailService.SendEmail(new EmailResponseDTO(
                        to: new string[] { model.Email },
                        subject: AppString.EmailConfirmationSubjectEmail,
                        content: AppString.EmailConfirmationContentEmail(emailConfirmationLink)
                    ));

                    return Ok(ResponseDTO.Accept(message: AppString.RegisterSuccessMessage));
                }
                return BadRequest(ResponseDTO.BadRequest(message: AppString.RegisterErrorMessage));
            }
            catch (Exception error)
            {
                return BadRequest(ResponseDTO.BadRequest(message: error.Message));
            }
        }

        [HttpPost("sign-in-facebook")]
        public async Task<IActionResult> SignInWithFacebook([FromBody] OAuth2RequestDTO oAuth2RequestDTO)
        {
            if (string.IsNullOrEmpty(oAuth2RequestDTO.AccessToken))
            {
                return BadRequest(ResponseDTO.BadRequest(message: AppString.InvalidTokenErrorMessage));
            }
            try
            {

                var token = await _authService.LoginWithThirdParty(oAuth2RequestDTO, ProviderLogin.FACEBOOK);

                if (token == null)
                {
                    return BadRequest(ResponseDTO.BadRequest(message: "Authentication failed!"));
                }

                LoginResponseDTO loginResponseDTO = new()
                {
                    AccessToken = token
                };

                return Ok(ResponseDTO.Accept(result: loginResponseDTO));
            }
            catch (Exception error)
            {
                return BadRequest(ResponseDTO.BadRequest(error.Message));
            }
        }

        [HttpPost("sign-in-google")]
        public async Task<IActionResult> SignInWithGoogle([FromBody] OAuth2RequestDTO oAuth2RequestDTO)
        {
            if (string.IsNullOrEmpty(oAuth2RequestDTO.AccessToken))
            {
                return BadRequest(ResponseDTO.BadRequest(message: AppString.InvalidTokenErrorMessage));
            }
            try
            {
                var token = await _authService.LoginWithThirdParty(oAuth2RequestDTO, ProviderLogin.GOOGLE);

                if (token == null)
                {
                    return BadRequest(ResponseDTO.BadRequest(message: "Authentication failed!"));
                }

                LoginResponseDTO loginResponseDTO = new()
                {
                    AccessToken = token
                };

                return Ok(ResponseDTO.Accept(result: loginResponseDTO));
            }
            catch (Exception error)
            {
                return BadRequest(ResponseDTO.BadRequest(error.Message));
            }
        }
    }
}
