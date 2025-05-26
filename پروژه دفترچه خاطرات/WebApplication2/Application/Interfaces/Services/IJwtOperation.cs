using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApplication2.Application.Interfaces.Services
{
    public interface IJwtOperation
    {
        public string RefreshToken();
        public SigningCredentials GetSigningCredentials();
        public  Task<string> GenerateTokenAsync(string UserName, int UserId);
        public Task<List<Claim>> GetClaimsAsync(string UserName, int UserId);
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);

    }
}
