namespace DFramework.Contracts.Localization
{
    public record LocalizedKeyDto
    {
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
