namespace ToyShopAPI.Models
{
    public class CreateOrderModel
    {
        public string userId { get; set; }
        public class OrderItems
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
        }

        public IEnumerable<OrderItems> Items{ get; set; }
    }
}
