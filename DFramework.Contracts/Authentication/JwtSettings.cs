namespace DFramework.Contracts.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public int ExpiryMinutes { get; set; }
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}
