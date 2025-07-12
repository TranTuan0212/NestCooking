using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NESTCOOKING_API.Business.Authorization
{
    public interface IJwtUtils
    {
        Task<string> GenerateJwtToken(User user);
        string? ValidateJwtToken(string? token);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly IRoleRepository _roleRepository;
        private readonly string secretKey;

        public JwtUtils(IRoleRepository roleRepository, IConfiguration configuration)
        {
            _roleRepository = roleRepository;
            secretKey = configuration["APPSETTING_API_SECRET"];
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var role = await _roleRepository.GetRoleNameByIdAsync(user.RoleId);

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(7).AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string? ValidateJwtToken(string? token)
        {
            if (token == null)
                return null;

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
                // return user's id from JWT token if validation successful
                if (!string.IsNullOrEmpty(userId))
                {
                    return userId;
                }
            }
            catch
            {
            }
            return null;
        }
    }
}
