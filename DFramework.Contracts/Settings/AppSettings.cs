using System;
namespace DFramework.Contracts.Settings
{
	public record AppSettings
	{
		public static string SectionName = "AppSettings";

		public string DefaultPassword { get; set; } = null!;
	}
}

