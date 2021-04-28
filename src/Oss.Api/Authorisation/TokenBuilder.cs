using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Oss.Core.Models;

namespace Oss.Api
{
    public class TokenBuilder : ITokenBuilder
    {
        // public string Token { get; set; }
        // public bool IsAuthorised { get; set; }
        // public List<string> Errors { get; set; }

        // TODO Consider passing only user.Id instead of kitchen sinking the user object
        public string Build(User user, String secret)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            // we define our token descriptor
            // We need to utilise claims which are properties in our token which gives information about the token
            // which belong to the specific user who it belongs to
            // so it could contain their id, name, email the good part is that these information
            // are generated by our server and identity framework which is valid and trusted
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("id", user.Id),
                        // new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        // new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        // the JTI is used for our refresh token which we will be covering in the next video
                        // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(1),
                // here we are adding the encryption alogorithim information which will be used to decrypt our token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}