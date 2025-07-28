using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NESTCOOKING_API.Business.Authorization;
using NESTCOOKING_API.Business.DTOs;
using NESTCOOKING_API.Business.DTOs.AuthDTOs;
using NESTCOOKING_API.Business.Exceptions;
using NESTCOOKING_API.Business.Services.IServices;
using NESTCOOKING_API.DataAccess.Models;
using NESTCOOKING_API.DataAccess.Repositories.IRepositories;
using NESTCOOKING_API.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using static NESTCOOKING_API.Utility.StaticDetails;

namespace NESTCOOKING_API.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public AuthService(IUserRepository userRepository, IJwtUtils jwtUtils, IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _userManager = userManager;
        }



        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _userRepository.Login(loginRequestDTO.UserName, loginRequestDTO.Password);

            if (user == null)
            {
                return null;
            }

            if (!_userManager.IsEmailConfirmedAsync(user).Result)
            {
                throw new EmailNotConfirmedException(AppString.NotEmailConfirmedErrorMessage);
            }
            if (_userManager.IsLockedOutAsync(user).Result)
            {
                throw new Exception(AppString.LockoutAccountErrorMessage);
            }

            LoginResponseDTO loginResponseDTO = new()
            {
                AccessToken = await _jwtUtils.GenerateJwtToken(user)
            };

            return loginResponseDTO;
        }
        public async Task<(string, string)> GenerateEmailConfirmationTokenAsync(string identifier)
        {
            User? user;
            var email = new EmailAddressAttribute();

            if (email.IsValid(identifier))
            {
                user = await _userManager.FindByEmailAsync(identifier);
            }
            else
            {
                user = await _userManager.FindByNameAsync(identifier);
            }

            if (user == null)
            {
                throw new Exception(AppString.UserNotFoundMessage);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception(AppString.SomethingWrongMessage);
            }

            return (email: user.Email, token);
        }

        public async Task<bool> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            if (!_userRepository.IsUniqueUserName(registrationRequestDTO.UserName))
            {
                throw new Exception("Your username is already exist!");
            }
            if (!_userRepository.IsUniqueEmail(registrationRequestDTO.Email))
            {
                throw new Exception("Your email is already exist!");
            }

            var newUser = _mapper.Map<User>(registrationRequestDTO);
            newUser.CreatedAt = DateTime.UtcNow.AddHours(7);
            var result = await _userRepository.Register(newUser, registrationRequestDTO.Password);

            return result;
        }
    }
}
