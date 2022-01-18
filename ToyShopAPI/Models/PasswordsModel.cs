namespace ToyShopAPI.Models
{
    public class PasswordsModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public string WebsiteName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
