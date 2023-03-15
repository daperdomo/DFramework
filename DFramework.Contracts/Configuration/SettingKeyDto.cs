namespace DFramework.Contracts.Configuration
{
    public record SettingKeyDto
    {
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
