using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace ToyShopAPI.Models
{
    public class UserModel:IdentityUser
    {
       // public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
