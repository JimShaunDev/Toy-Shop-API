using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToyShopAPI.Models;

namespace ToyShopAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PasswordsModel> Passwords { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
