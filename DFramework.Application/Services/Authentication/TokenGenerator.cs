using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Contracts.Authentication;
using DFramework.Contracts.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DFramework.Application.Services.Authentication
{
    internal class TokenGenerator : ITokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        public TokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<string> GenerateToken(AuthenticateResponse request)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, request.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, request.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials,
                audience: _jwtSettings.Audience);

            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(securityToken));
        }
    }
}
