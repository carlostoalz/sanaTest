namespace SanaTest.BE
{
    public class OrderDTO
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
