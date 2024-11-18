namespace RetrieveJWTToken.Models
{
    public class JWTTokenRequest
    {        
        public string? grant_type { get; set; }
        public string? id { get; set; }
        public string? key { get; set; }
    }
}
