using System.Text.Json;

namespace SanaTest.BE
{
    public class ProductDTO : Product
    {
        public string Categories { get;set; }
        public int Quantity { get; set; }
    }
}
