namespace ToyShopAPI.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderModel Order { get; set; }

        public int ProductId {get; set;}
        public ProductModel Product { get; set; }

        public int quantity { get; set; }


    }
}
