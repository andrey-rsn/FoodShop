using FoodShop.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.ProductAPI.DBContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base (options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
