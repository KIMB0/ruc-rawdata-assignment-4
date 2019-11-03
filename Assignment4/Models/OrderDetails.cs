namespace Assignment4.Tests
{
    public class OrderDetails
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
    }
}