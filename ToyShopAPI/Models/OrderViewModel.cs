namespace ToyShopAPI.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

       
       public int ProductId { get; set; }
       public string ProductName { get; set; }
       public int Quantity { get; set; }

       public int Price { get; set; }
        

        
        public double OrderTotal { get; set; }
    }
}
