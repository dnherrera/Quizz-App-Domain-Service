using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace QuizApp.API.Helpers
{
    /// <summary>
    /// Token String
    /// </summary>
    public class TokenString
    {
        /// <summary>
        /// Generate Token String
        /// </summary>
        /// <param name="userIdentifier">The user's identifier </param>
        /// <param name="userName">The username</param>
        /// <param name="tokenKey">The token key from appsettings </param>
        /// <param name="tokenString">The out return. </param>
        public static void GenerateTokenString(string userIdentifier, string userName, string tokenKey,  out string tokenString)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(tokenKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userIdentifier),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha512Signature
                )
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            tokenString = tokenHandler.WriteToken(token);
        }
    }
}
