namespace Core.Utilities.Security.JWT
{
    // Entity olmadığı için çoğul (IDto vs. değil)
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
