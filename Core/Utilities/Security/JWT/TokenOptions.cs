namespace Core.Utilities.Security.JWT
{
    public partial class JwtHelper
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
}
