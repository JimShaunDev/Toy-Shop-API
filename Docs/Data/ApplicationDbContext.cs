using Microsoft.EntityFrameworkCore;
using ToyShopAPI.Models;

namespace ToyShopAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
