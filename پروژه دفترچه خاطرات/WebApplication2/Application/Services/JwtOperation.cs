using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Application.Interfaces.Services;

namespace WebApplication2.Application.Services
{
    public class JwtOperation : IJwtOperation
    {
        private IConfiguration _configuration;
        private IConfigurationSection _jwtsetting;
        public JwtOperation(IConfiguration configuration)
        {
            this._configuration = configuration;
            _jwtsetting = _configuration.GetSection("JwtSettings");

        }
        public string RefreshToken()
        {
            var RandomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(RandomNumber);
            return Convert.ToBase64String(RandomNumber);
        }

        public SigningCredentials GetSigningCredentials()
        {
            var values = _jwtsetting.GetValue<string>("SecretKey");
            var key = Encoding.UTF8.GetBytes(values);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<string> GenerateTokenAsync(string UserName, int UserId)
        {
            var signingcredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(UserName: UserName, UserId: UserId);
            var tokenoptions = GenerateTokenOptions(signingcredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenoptions);
        }

        public async Task<List<Claim>> GetClaimsAsync(string UserName, int UserId)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("UserName", UserName));
            claims.Add(new Claim("UserId", UserId.ToString()));
            return await Task.FromResult(claims);
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var TokenOptions = new JwtSecurityToken(

                issuer : _jwtsetting.GetValue<string>("Issuer"),
                audience : _jwtsetting.GetValue<string>("Audience"),
                claims : claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_jwtsetting.GetValue<string>("ExpiresInDays"))),
                signingCredentials: signingCredentials);
                return TokenOptions; 
        }
    }
}


