using RetrieveJWTToken.Models;

namespace RetrieveJWTToken.Services
{
    public interface IJWTTokenService
    {
        Task<JWTTokenResponse> JWTTokenRequestAsync(JWTTokenRequest tokenRequest);
    }
}
