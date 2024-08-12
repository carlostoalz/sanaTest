using System.Diagnostics.CodeAnalysis;

namespace SanaTest.BE
{
    [ExcludeFromCodeCoverage]
    public class Order
    {
        public int Id { get; set; }
        public int Id_customer { get; set; }
        public int Total_products { get; set; }
        public decimal Total_price { get; set; }
    }
}
