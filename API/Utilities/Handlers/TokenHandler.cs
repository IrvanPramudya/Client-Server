using API.Contracts;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace API.Utilities.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        public readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GenerateToken(IEnumerable<Claim> claims)
        {
            var secretkey           = new SymmetricSecurityKey(Encoding.UTF8
                                                             .GetBytes(_configuration["JWTConfig:SecretKey"]));         //mengubah secretkey kedalam bentuk byte
            var signincredential    = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);                 //untuk mengekripsi dengan menggunakan algoritma yang akan digunakan
            var tokenoption         = new JwtSecurityToken(issuer               : _configuration["JWTConfig:Issuer"],
                                                           audience             : _configuration["JWTConfig:Audience"],
                                                           claims               : claims,
                                                           expires              : DateTime.Now.AddMinutes(10),
                                                           signingCredentials   : signincredential);
            var token               = new JwtSecurityTokenHandler().WriteToken(tokenoption);
            return token;
        }
    }
}
