using System.Diagnostics.CodeAnalysis;

namespace SanaTest.BE
{
    [ExcludeFromCodeCoverage]
    public class AppSetting
    {
        public string DbConnection { get; set; }
        public Procedures Procedures { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class Procedures
    {
        public string GetProducts { get; set; }
        public string CreateOrder { get; set; }
    }
}