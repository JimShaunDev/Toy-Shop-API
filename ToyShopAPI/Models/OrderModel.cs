namespace ToyShopAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }

        public DateTime OrderDate { get; set; }
        public bool IsReady { get; set; }
        public bool Collected { get; set; }


    }
}
