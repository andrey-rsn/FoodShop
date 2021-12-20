using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.ShoppingCartAPI.DBContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

    }
}
