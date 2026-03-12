using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Token
{
    public class TokenTools
    {
        // DI
        private readonly IConfiguration _config;

        public TokenTools(IConfiguration config)
        {
            _config = config;
        }

        // Container for Token Data
        public class Data
        {
            public required Guid MemberId { get; set; }
        }

        // Token Gen Process
        public string Generate(Data data)
        {
            // Object "Claim" w. Token's data
            Claim[] claims = [
                new Claim("Clef", "Valeur"),
                new Claim(ClaimTypes.NameIdentifier, data.MemberId.ToString()),
            ];

            // Token Signature
            byte[] key = Encoding.UTF8.GetBytes(_config["Token:Key"]!);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Token:Issuer"],                                                // Token issuer ID
                audience: _config["Token:Audience"],                                            // Expected context of use
                expires: DateTime.Now.AddMinutes(_config.GetValue<int>("Token:Expires")),       // Validity duration
                claims: claims,                                                                 // Data
                signingCredentials: signingCredentials                                          // Signature
            );

            // Return Token as string
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
