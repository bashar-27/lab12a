using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace lab12a.Models.Services
{
    public class JwtTokenService
    {
        private SignInManager<AppUser> signInManager;
        private IConfiguration configuration;
        public JwtTokenService(IConfiguration config,SignInManager<AppUser> manager)
        {
            this.configuration = config;
            this.signInManager = manager;
            
        }
        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false

            };
        }
        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null) {
                throw new InvalidOperationException("JWT:Secret is not exist!!");
            }
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            return new SymmetricSecurityKey(secretBytes);

        }
        public async Task<string> GetToken(AppUser user,TimeSpan expire)
        {
            var prinsple= await signInManager.CreateUserPrincipalAsync(user);
            if(prinsple == null)
            {
                return null;
            }
            var singninKey = GetSecurityKey(configuration);
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow + expire,
                signingCredentials: new SigningCredentials(singninKey, SecurityAlgorithms.HmacSha256),
               claims: prinsple.Claims);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
