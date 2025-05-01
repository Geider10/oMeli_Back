using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Globalization;
namespace oMeli_Back.Utils
{
    public class Util
    {
        private readonly IConfiguration _configuration;
        public Util(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string HastText(string text)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(text);
            return hash;
        }
        public bool VerifyHashText(string text, string hastText)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(text, hastText);
            return isValid;
        }
        public string GenerateToken(string id, List<string> roles)
        {
            var userClaims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, id),
            };
            userClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var secreteKey = _configuration.GetRequiredSection("SECRET_KEY").Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreteKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //create token
            var token = new JwtSecurityToken(
                claims: userClaims,
                expires : DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime ConvertDate(string date)
        {
            var newDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return newDate;
        }
    }

}
