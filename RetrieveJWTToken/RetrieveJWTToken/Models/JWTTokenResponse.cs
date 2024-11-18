namespace RetrieveJWTToken.Models
{
    public class JWTTokenResponse
    {
        public string? token { get; set; }
        public string? type { get; set; }
        public string? issued { get; set; }
        public string? expired { get; set; }
    }
}
