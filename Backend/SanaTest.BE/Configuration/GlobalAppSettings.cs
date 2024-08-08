using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace SanaTest.BE
{
    [ExcludeFromCodeCoverage]
    public class GlobalAppSettings
    {
        public AppSetting Settings { get; set; }
        public GlobalAppSettings(IOptions<AppSetting> settings) => this.Settings = settings.Value;
        public GlobalAppSettings() { }
    }
}