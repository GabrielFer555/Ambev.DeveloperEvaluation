using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Ambev.DeveloperEvaluation.Integration.Products;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ambev.DeveloperEvaluation.Integration.Auth
{
	public static class AuthHandler
	{

        public static string GenerateJwtToken()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ProductControllerTests>()
                .Build();

            var secretKey = configuration["Jwt:SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("JWT Secret Key is missing. Ensure it is set in User Secrets.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, "test-user"),
        new Claim(ClaimTypes.Name, "Test User"),
        new Claim(ClaimTypes.Role, "Admin")
    };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
