using System.Diagnostics.CodeAnalysis;

namespace SanaTest.BE
{
    [ExcludeFromCodeCoverage]
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public string Img { get; set; }
    }
}
