using FoodShop.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.ShoppingCartAPI.DBContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartHeader> CartHeaders { get; set; }

        public DbSet<CartDetails> CartDetails { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
