
using Microsoft.IdentityModel.Tokens;
using RetrieveJWTToken.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetrieveJWTToken.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        public async Task<JWTTokenResponse> JWTTokenRequestAsync(JWTTokenRequest tokenRequest)
        {
            // Define the symmetric security key (signing key) using the secret from Kong
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zEuRjvLxtn1dMCf2dga6lnZjv6MXEpMO"));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenRequest.key));

            // Define the signing credentials with HMAC SHA256
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenexpired1 = DateTimeOffset.UtcNow.AddMinutes(30).ToString();
            var tokenexpired = DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds().ToString();

            // Define the claims required for Kong
            var claims = new[]
            {                
                new Claim(JwtRegisteredClaimNames.Sub, "UCTS token retrieval"), // 'sub' claim: Identifier for the consumer (user or app)                               
                new Claim(JwtRegisteredClaimNames.Exp, tokenexpired) // Token expiration
            };

            // Create the token descriptor
            var tokenDescriptor = new JwtSecurityToken(
                //issuer: "tOeiKTU1IIdRwG4jJlZYOdE6ZIYx8QMC", // The 'iss' claim
                issuer: tokenRequest.id, // The 'iss' claim
                                                            //audience: "test3", // The 'aud' claim
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30), // Expiration time
                signingCredentials: credentials // Signing credentials                
            );

            //tokenDescriptor.Header.Add("iss", "tOeiKTU1IIdRwG4jJlZYOdE6ZIYx8QMC");
            tokenDescriptor.Header.Add("iss", tokenRequest.id);

            // Create a token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Generate and return the JWT token

            var jwtTokenResponse = new JWTTokenResponse
            {
                token = tokenHandler.WriteToken(tokenDescriptor),
                type = "bearer",
                issued = DateTime.UtcNow.ToString(),
                expired = tokenexpired1
            };

            return jwtTokenResponse;
        }
    }
}
